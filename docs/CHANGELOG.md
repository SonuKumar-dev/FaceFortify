# Changelog

All notable changes to FaceFortify Desktop will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Planned
- Multi-user support
- Timed automatic locking
- Cloud backup (encrypted)
- Biometric integration (Windows Hello, Touch ID)

---

## [1.0.0] - 2025-01-XX

### Added
- ✨ Initial release of FaceFortify Desktop
- 🔐 AES-256-CBC encryption for folders
- 👤 ArcFace-based facial recognition
- 👁️ Eye detection validation with Haar Cascades
- 📹 Real-time camera preview with visual feedback
- 🔑 Hardware-bound encryption keys
- 🔒 Optional backup password system
- 💾 Cross-platform support (Windows, Linux, macOS)
- 🎨 Modern Avalonia UI with Fluent design
- 📂 Folder lock/unlock functionality
- ⚙️ User profile management
- 🌐 OS-standard data storage locations
- 🛡️ Multi-frame face validation (30 frames)
- 📊 Confidence score display
- 🔄 PBKDF2 key derivation (100k iterations)

### Security
- Implemented zero-knowledge architecture
- Added hardware ID binding for keys
- Enabled platform-specific access control (ACL/chmod/chflags)
- Added secure key caching with auto-clear
- Implemented salt persistence for key compatibility

### Platform-Specific
- **Windows**: Inno Setup installer, Start Menu integration
- **Linux**: AppImage with desktop integration
- **macOS**: DMG with both Intel and Apple Silicon support

### Documentation
- Comprehensive README with badges
- Build instructions for all platforms
- Security policy and threat model
- Contributing guidelines
- Code of conduct

---

## [0.9.0-beta] - 2025-01-XX

### Added
- Beta release for testing
- Core facial recognition functionality
- Basic encryption support
- Windows-only support

### Known Issues
- Eye detection not visualized in camera preview
- Data stored in bin/Debug folder (not production-ready)
- No installers available

---

## Development Milestones

### Phase 1: Core Functionality ✅
- [x] Facial recognition system
- [x] Encryption implementation
- [x] Folder locking mechanism
- [x] User interface

### Phase 2: Cross-Platform ✅
- [x] Linux support
- [x] macOS support
- [x] Platform-specific access control
- [x] OS-standard data storage

### Phase 3: Polish & Distribution ✅
- [x] Eye detection visualization
- [x] Build scripts for all platforms
- [x] Installers (Windows, Linux, macOS)
- [x] Documentation
- [x] Icons and branding

### Phase 4: Future Enhancements 🔄
- [ ] Multi-user support
- [ ] Auto-lock timers
- [ ] Cloud sync (encrypted)
- [ ] Mobile app integration
- [ ] Hardware key support

---

## Version History

### 1.0.0 - Initial Release
First stable release with full cross-platform support and production-ready builds.

### 0.9.0-beta - Beta Testing
Beta release for early adopters and testing.

---

## Migration Guide

### From Beta to 1.0.0

**Data Location Changes:**
- Old: `bin/Debug/net8.0/Data/`
- New: OS-specific locations
  - Windows: `%APPDATA%\FaceFortify`
  - Linux: `~/.local/share/FaceFortify`
  - macOS: `~/Library/Application Support/FaceFortify`

**Migration Steps:**
1. Export your locked folders list (manually note them down)
2. Unlock all folders in beta version
3. Uninstall beta version
4. Install v1.0.0
5. Re-register your face
6. Add folders back and lock them

**Note**: Face data is NOT compatible between versions. You'll need to re-register.

---

## Breaking Changes

### 1.0.0
- Data storage location changed (see migration guide)
- Face data format updated (requires re-registration)
- Removed debug logging in release builds

---

## Deprecations

None yet.

---

## Security Updates

### 1.0.0
- Initial security implementation
- No CVEs to report

---

## Performance Improvements

### 1.0.0
- Optimized face detection (Haar Cascade + eye validation)
- Improved key caching
- Faster folder encryption with parallel processing (planned for 1.1)

---

## Bug Fixes

### 1.0.0
- Fixed Assets folder not copying to publish directory
- Fixed installer access violation error (simplified Inno Setup script)
- Fixed eye detection not showing in camera preview
- Fixed data path issues across platforms

---

## Contributors

Thank you to all contributors! 🎉

- @yourusername - Project creator and maintainer
- @contributor1 - Feature X
- @contributor2 - Bug fix Y

[View all contributors](https://github.com/yourusername/FaceFortify/graphs/contributors)

---

## Links

- [Source Code](https://github.com/SonuKumar-dev/FaceFortify)
- [Issue Tracker](https://github.com/SonuKumar-dev/FaceFortify/issues)
- [Discussions](https://github.comSonuKumar-dev/FaceFortify/discussions)
- [Releases](https://github.com/SonuKumar-dev/FaceFortify/releases)

---

[Unreleased]: https://github.com/SonuKumar-dev/FaceFortify/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/SonuKumar-dev/FaceFortify/releases/tag/v1.0.0
[0.9.0-beta]: https://github.com/SonuKumar-dev/FaceFortify/releases/tag/v0.9.0-beta
