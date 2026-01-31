# FaceFortify Algorithms

This document describes the core algorithms used in FaceFortify for face recognition, key derivation, and file encryption.

## System Flow

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     User Interface (Avalonia UI)             │
│  LandingPage → MainWindow → CameraWindow → Settings         │
└────────────────────────────┬────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────┐
│                     ViewModels (MVVM)                        │
│  - LandingPageViewModel                                      │
│  - MainWindowViewModel                                       │
│  - CameraWindowViewModel                                     │
│  - SettingsWindowViewModel                                   │
└────────────────────────────┬────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────┐
│                        Services Layer                        │
│  ┌─────────────────┐  ┌──────────────────┐                 │
│  │ Face Services   │  │ Crypto Services  │                 │
│  │ - FaceAuth      │  │ - AesKey         │                 │
│  │ - ArcFace       │  │ - KeyDerivation  │                 │
│  │ - HaarCascade   │  │ - FolderCrypto   │                 │
│  │ - Camera        │  │ - FolderLock     │                 │
│  └─────────────────┘  └──────────────────┘                 │
│                                                              │
│  ┌─────────────────┐  ┌──────────────────┐                 │
│  │ System Services │  │ Data Services    │                 │
│  │ - MachineId     │  │ - UserProfile    │                 │
│  │ - AccessControl │  │ - AppState       │                 │
│  │ - DataPath      │  │                  │                 │
│  └─────────────────┘  └──────────────────┘                 │
└─────────────────────────────────────────────────────────────┘
```

## 1. Face Registration Algorithm

1. Capture 30 frames from the camera.
2. For each frame:
   - Detect face using Haar Cascade.
   - Validate eyes are open (anti-spoofing).
   - Extract face region and resize to model input size.
   - Generate a 512-dimensional embedding using ArcFace (ONNX).
3. Discard invalid or low-quality frames.
4. Compute the average of all valid embeddings.
5. Apply L2 normalization to the final vector.
6. Encrypt and store as `face.dat`.

Result: A stable biometric template representing the user’s face.

## 2. Face Verification Algorithm

1. Capture 10 frames from the camera.
2. Generate embeddings for each frame.
3. For each embedding:
   - Compute cosine similarity with stored embedding.
4. Count matches where similarity ≥ 0.45.
5. If ≥ 60% frames match → verification success.

This multi-frame voting prevents false positives and spoofing.

## 3. Key Derivation Algorithm

Inputs:
- Face embedding (512 floats)
- Hardware ID (machine fingerprint)
- 256-bit random salt

Steps:
1. Concatenate all inputs.
2. Apply PBKDF2-HMAC-SHA256 with 100,000 iterations.
3. Output 256-bit key.

This ensures:
- Same face + same machine = same key
- Different machine = different key

## 4. File Encryption Algorithm

1. Generate random 128-bit IV.
2. Encrypt file using AES-256-CBC with PKCS7 padding.
3. Save output as:
4. Overwrite original file with encrypted data.

Each file uses a unique IV for security.

## 5. Hardware ID Generation

Platform specific data is collected:

- Windows: CPU ID, motherboard serial, MAC
- Linux: machine-id, CPU info, MAC
- macOS: hardware UUID, serial, MAC

Steps:
1. Concatenate identifiers.
2. Compute SHA-256 hash.
3. Encode as Base64 → Machine ID.

This binds encryption keys to one device.
### Why Cosine Similarity?

Face embeddings are **normalized vectors**. Cosine similarity measures the angle between vectors:

```
similarity = cos(θ) = (A · B) / (||A|| × ||B||)

Since both are L2 normalized (||A|| = ||B|| = 1):
similarity = A · B  (just the dot product!)

Range: -1 to 1
• 1.0 = Identical
• 0.5 = Different but similar
• 0.0 = Completely different
• -1.0 = Opposite

Our threshold: 0.45 = ~63° angle difference
```

---

## Encryption Key Derivation

### The Key Derivation Chain

```
┌─────────────────────────────────────────────────────────────┐
│                    INPUT COMPONENTS                          │
└────────────┬────────────────────────────────────────────────┘
             │
   ┌─────────┴─────────┬─────────────┬─────────────────┐
   │                   │             │                 │
   ▼                   ▼             ▼                 ▼
┌──────┐         ┌──────────┐   ┌──────┐       ┌──────────┐
│ Face │         │ Machine  │   │ Salt │       │ Password │
│Embed │   +     │    ID    │   │(32B) │       │(optional)│
│512 F │         │ (Base64) │   │Random│       │          │
└──────┘         └──────────┘   └──────┘       └──────────┘
   │                   │             │                 │
   │                   │             │                 │
   └─────────┬─────────┘             │                 │
             │                       │                 │
             ▼                       │                 │
      ┌─────────────┐                │                 │
      │  Combined   │                │                 │
      │   Bytes     │                │                 │
      │ (Float→Byte)│                │                 │
      └──────┬──────┘                │                 │
             │                       │                 │
             └───────────┬───────────┘                 │
                         │                             │
                         ▼                             │
                  ┌─────────────┐                      │
                  │   PBKDF2    │                      │
                  │ Key         │                      │
                  │ Derivation  │                      │
                  │             │                      │
                  │ • Algo: SHA256                     │
                  │ • Iterations: 100,000              │
                  │ • Output: 32 bytes                 │
                  └──────┬──────┘                      │
                         │                             │
                         ▼                             │
                  ┌─────────────┐                      │
                  │  Primary    │                      │
                  │  AES Key    │                      │
                  │  (256-bit)  │                      │
                  └─────────────┘                      │
                                                       │
      Face + Password (no machine ID)                 │
      ────────────────────────────────────────────────┘
                         │
                         ▼
                  ┌─────────────┐
                  │   Backup    │
                  │  AES Key    │
                  │  (256-bit)  │
                  └─────────────┘
```

### PBKDF2 in Detail

**Purpose**: Slow down brute-force attacks

```python
# Simplified pseudocode
def PBKDF2(password, salt, iterations, hash_algo, key_length):
    # Initial state
    result = bytes()
    
    # For each block of output needed
    for block in range(1, ceil(key_length / hash_length) + 1):
        # First iteration: PRF(password, salt || block)
        u = PRF(password, salt + int_to_bytes(block))
        current = u
        
        # Remaining iterations
        for i in range(1, iterations):
            u = PRF(password, u)
            current = current XOR u  # XOR results together
        
        result += current
    
    return result[:key_length]
```

**Why 100,000 iterations?**
- Makes each key derivation take ~50-100ms
- Slows attackers to ~10-20 attempts/second
- Users barely notice the delay
- NIST recommendation: ≥10,000 (we use 10x more!)

**Example timing:**
```
1 iteration:     0.001 ms  →  1,000,000 attempts/sec  ❌
100 iterations:  0.1 ms    →  10,000 attempts/sec      ❌
100,000 iterations: 100 ms →  10 attempts/sec          ✅
```



