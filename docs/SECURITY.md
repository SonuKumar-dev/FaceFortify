# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |
| < 1.0   | :x:                |

## Security Best Practices

### For Users

1. **Keep Software Updated**
   - Update to the latest version promptly
   - Security patches are released as needed

2. **Use Strong OS Password**
   - FaceFortify protects files, but your OS password is the first line of defense
   - Use a strong, unique password for your user account

3. **Set Backup Password**
   - Set a backup password for hardware change recovery
   - Store it securely (password manager recommended)

4. **Lock Your Computer**
   - Always lock your computer when away
   - FaceFortify assumes physical security

5. **Backup Important Data**
   - Encryption is strong - lost keys = lost data
   - Keep backups of critical files before encrypting

### For Developers

1. **Code Review**
   - All security-sensitive code requires review
   - Cryptographic operations are double-checked

2. **Dependencies**
   - Regularly update dependencies
   - Monitor security advisories

3. **Testing**
   - Security features have extensive tests
   - Manual testing on all supported platforms

4. **Secure Development**
   - Follow OWASP guidelines
   - Use static analysis tools
   - Never commit secrets

## Known Limitations

### Not Protected Against

1. **Keyloggers**
   - Malware with OS-level access can capture input
   - Mitigation: Use antivirus, keep OS updated

2. **Physical Coercion**
   - "Wrench attack" (XKCD 538)
   - Mitigation: None (human safety > data security)

3. **State-Level Adversaries**
   - Three-letter agencies with unlimited resources
   - Mitigation: This tool isn't designed for this threat model

4. **Quantum Computers (Future)**
   - AES-256 may be vulnerable to quantum attacks
   - Mitigation: Will upgrade to post-quantum algorithms when standardized

### Design Decisions

**Why Local-Only Storage?**
- No attack surface from internet
- Complete user control
- Privacy by design
- Trade-off: No sync across devices

**Why Hardware-Bound Keys?**
- Prevents key extraction
- Stronger security posture
- Trade-off: Hardware changes require backup password

**Why Optional Backup Password?**
- User choice between convenience and recovery
- Not forced (some users don't need it)
- Trade-off: Users must remember/store it securely

## Encryption Details

### Algorithm: AES-256-CBC

**Strengths:**
- NIST-approved standard
- 256-bit keys = ~2^256 possible combinations
- Computationally infeasible to brute force

**Implementation:**
- Random IV per file
- PBKDF2 for key derivation
- 100,000 iterations (SHA-256)
- Proper IV prepending

### Key Derivation: PBKDF2

**Parameters:**
- Algorithm: SHA-256
- Iterations: 100,000
- Salt: 256-bit random
- Output: 256-bit key

**Why PBKDF2?**
- Slows down brute-force attacks
- Widely supported
- NIST-approved

### Face Embedding

**Model: ArcFace**
- 512-dimensional embedding
- L2 normalized
- Similarity threshold: 0.45
- Multi-frame averaging for stability

**Storage:**
- Embeddings are hashed before storage
- Never stored in plain text
- Used only for key derivation

## Third-Party Security

### Dependencies

All dependencies are regularly audited:

- **Avalonia UI** - UI framework (MIT License)
- **OpenCV Sharp** - Computer vision (Apache 2.0)
- **ONNX Runtime** - ML inference (MIT License)

### Supply Chain

- Dependencies pinned to specific versions
- NuGet package signatures verified
- Regular dependency updates
- Automated vulnerability scanning

## Audit Trail

### Security Audits

| Date | Auditor | Scope | Findings |
|------|---------|-------|----------|
| TBD  | TBD     | TBD   | TBD      |

*We welcome independent security audits. Contact us if interested.*

### Vulnerability Disclosure

| CVE ID | Severity | Fixed In | Description |
|--------|----------|----------|-------------|
| N/A    | N/A      | N/A      | No vulnerabilities reported yet |

## Contact

- **Bug Reports**: GitHub Issues

**Thank you for keeping FaceFortify secure! 🔒**
