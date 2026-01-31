# FaceFortify Architecture

FaceFortify follows a modular, MVVM-based architecture.

## High Level Components

## Modules

### 1. UI Layer (Views)

- Built using Avalonia UI
- XAML-based cross-platform interface
- No business logic

### 2. ViewModels

- Handle user interactions
- Bind UI to services
- Commands: RegisterFace, LockFolder, UnlockFolder, etc.

### 3. Services

Core logic is implemented here:

- `FaceRecognitionService`
  - Camera capture
  - Face detection
  - ArcFace embedding extraction
  - Verification

- `CryptoService`
  - PBKDF2 key derivation
  - AES file encryption/decryption

- `HardwareService`
  - Collects machine identifiers
  - Generates hardware-bound ID

- `StorageService`
  - Read/write `face.dat`, `keyinfo.dat`, `folders.json`

### 4. Models

Simple data structures:

- UserProfile
- ProtectedFolder
- KeyInfo
- FaceEmbedding

## Data Flow Example (Unlock Folder)

1. User clicks Unlock
2. View → ViewModel command
3. ViewModel calls FaceRecognitionService.Verify()
4. If success:
   - CryptoService derives key
   - CryptoService decrypts files
5. ViewModel updates UI state

## Security Principles

- No plaintext keys stored
- Keys derived only when needed
- Sensitive buffers cleared from memory after use
- All biometric data stored locally only
