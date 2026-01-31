# FaceFortify Algorithms

This document describes the core algorithms used in FaceFortify for face recognition, key derivation, and file encryption.

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

