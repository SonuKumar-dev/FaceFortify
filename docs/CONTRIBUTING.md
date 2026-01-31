# Contributing to FaceFortify Desktop

First off, thank you for considering contributing to FaceFortify! 🎉

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
- [Development Setup](#development-setup)
- [Pull Request Process](#pull-request-process)
- [Style Guidelines](#style-guidelines)
- [Community](#community)

---

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code.

### Our Pledge

- Be respectful and inclusive
- Welcome newcomers
- Accept constructive criticism
- Focus on what's best for the community

---

## How Can I Contribute?

### 🐛 Reporting Bugs

**Before Submitting:**
- Check existing [issues](https://github.com/yourusername/FaceFortify/issues)
- Update to the latest version
- Verify it's not a configuration issue

**Submit a Bug Report:**
1. Use the bug report template
2. Include steps to reproduce
3. Add screenshots/logs if applicable
4. Specify OS and version

### 💡 Suggesting Features

**Before Suggesting:**
- Check [discussions](https://github.com/yourusername/FaceFortify/discussions)
- Check existing feature requests
- Consider if it aligns with project goals

**Submit a Feature Request:**
1. Use the feature request template
2. Explain the use case
3. Describe the solution you'd like
4. Note any alternatives considered

### 📝 Improving Documentation

- Fix typos or clarify instructions
- Add examples or tutorials
- Translate to other languages
- Update screenshots

### 🔧 Code Contributions

Areas we need help:
- Cross-platform testing
- Performance optimization
- UI/UX improvements
- Security enhancements
- Test coverage

---

## Development Setup

### Prerequisites

```bash
# Required
- .NET 8 SDK
- Git
- Camera for testing

# Recommended
- Visual Studio 2022 or VS Code
- GitHub Desktop (optional)
```

### First Time Setup

1. **Fork and Clone**
   ```bash
   git clone https://github.com/YOUR-USERNAME/FaceFortify.Desktop.git
   cd FaceFortify.Desktop
   ```

2. **Download Models**
   - Download `arcface.onnx` to `Assets/models/`
   - Download Haar Cascades to `Assets/models/`
   - See [README](README.md#required-assets) for links

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Build**
   ```bash
   dotnet build
   ```

5. **Run**
   ```bash
   dotnet run
   ```

### Development Workflow

```bash
# Create feature branch
git checkout -b feature/your-feature-name

# Make changes
# Test thoroughly

# Commit
git add .
git commit -m "Add: Your feature description"

# Push
git push origin feature/your-feature-name

# Create Pull Request on GitHub
```

---

## Pull Request Process

### Before Submitting

- [ ] Code builds without errors
- [ ] All tests pass
- [ ] No new warnings
- [ ] Code follows style guidelines
- [ ] Documentation updated if needed
- [ ] Tested on target platform(s)

### PR Title Format

Use conventional commits:

```
Add: New feature description
Fix: Bug fix description  
Update: Documentation/dependency update
Refactor: Code improvement
Test: Add or update tests
```

### PR Description Template

```markdown
## Description
Brief description of changes

## Motivation
Why is this change needed?

## Changes
- Change 1
- Change 2

## Testing
How was this tested?

## Screenshots (if applicable)
Before/after screenshots

## Checklist
- [ ] Code builds
- [ ] Tests pass
- [ ] Documentation updated
- [ ] Tested on Windows/Linux/macOS
```

### Review Process

1. Automated checks must pass
2. At least one maintainer review required
3. Address feedback promptly
4. Squash commits before merge (optional)

---

## Style Guidelines

### C# Code Style

Follow standard C# conventions:

```csharp
// Use PascalCase for public members
public class FaceAuthService
{
    // Use camelCase for private fields with underscore
    private readonly ArcFaceRecognitionService _recognitionService;
    
    // Use clear, descriptive names
    public async Task<bool> VerifyFaceAsync(float[][] embeddings)
    {
        // Use early returns
        if (embeddings == null || embeddings.Length == 0)
            return false;
            
        // Use var for obvious types
        var result = await ProcessEmbeddingsAsync(embeddings);
        
        return result;
    }
}
```

### File Organization

```csharp
// 1. Using statements (grouped and sorted)
using System;
using System.Linq;
using System.Threading.Tasks;

// 2. Namespace
namespace FaceFortify.Desktop.Services;

// 3. Class with XML comments
/// <summary>
/// Service for facial authentication
/// </summary>
public class FaceAuthService
{
    // 4. Private fields
    // 5. Constructor
    // 6. Public properties
    // 7. Public methods
    // 8. Private methods
}
```

### Naming Conventions

- **Classes**: `PascalCase` (e.g., `FaceAuthService`)
- **Methods**: `PascalCase` (e.g., `VerifyFace`)
- **Properties**: `PascalCase` (e.g., `UserName`)
- **Private fields**: `_camelCase` (e.g., `_service`)
- **Local variables**: `camelCase` (e.g., `result`)
- **Constants**: `PascalCase` (e.g., `MaxAttempts`)

### Comments

```csharp
// Use XML comments for public APIs
/// <summary>
/// Verifies a face against stored embedding
/// </summary>
/// <param name="embeddings">Face embeddings to verify</param>
/// <returns>True if face matches</returns>
public bool VerifyFace(float[][] embeddings)
{
    // Use single-line comments for implementation details
    // Only comment non-obvious logic
    
    return embeddings.Length >= MinFrames;
}
```

### XAML Style

```xml
<!-- Use proper indentation -->
<Window xmlns="https://github.com/avaloniaui"
        Title="FaceFortify"
        Width="800"
        Height="600">
    
    <!-- Group related properties -->
    <Window.Styles>
        <!-- Styles here -->
    </Window.Styles>
    
    <!-- Clear hierarchy -->
    <Grid RowDefinitions="Auto,*,Auto">
        <TextBlock Grid.Row="0" 
                   Text="Header"
                   Classes="header" />
    </Grid>
</Window>
```

---

## Commit Messages

### Format

```
Type: Short description (50 chars max)

Longer description if needed (wrap at 72 chars)
- Detail 1
- Detail 2

Fixes #123
```

### Types

- **Add**: New feature
- **Fix**: Bug fix
- **Update**: Dependencies, docs, or version
- **Refactor**: Code improvement (no functional change)
- **Test**: Add or update tests
- **Docs**: Documentation only
- **Style**: Formatting (no code change)
- **Perf**: Performance improvement

### Examples

```
Add: Eye detection visualization in camera preview

- Added cyan circles on detected eyes
- Added eye count label
- Updated HaarCascade service to export eye data

Fixes #45
```

---

## Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter FullyQualifiedName~FaceAuthTests

# With coverage
dotnet test /p:CollectCoverage=true
```

### Writing Tests

```csharp
[Fact]
public void VerifyFace_WithValidEmbeddings_ReturnsTrue()
{
    // Arrange
    var service = new FaceAuthService();
    var embeddings = CreateTestEmbeddings();
    
    // Act
    var result = service.VerifyFace(embeddings);
    
    // Assert
    Assert.True(result);
}
```

---

## Documentation

### Code Documentation

- Use XML comments for all public APIs
- Include examples for complex methods
- Document exceptions
- Keep comments up-to-date

### README Updates

If your change affects:
- Installation
- Usage
- Requirements
- Configuration

Please update the appropriate sections in README.md

---

## Community

### Where to Get Help

- [GitHub Discussions](https://github.com/yourusername/FaceFortify/discussions) - General questions
- [Discord](#) - Real-time chat (if available)
- [Issues](https://github.com/yourusername/FaceFortify/issues) - Bug reports only

### Where to Discuss

- Feature ideas → Discussions
- Bug reports → Issues
- Pull requests → PR comments
- Security issues → security@yourproject.com (private)

---

## Recognition

Contributors are recognized in:
- CONTRIBUTORS.md file
- Release notes
- Special mentions for significant contributions

---

## Questions?

Don't hesitate to ask! We're here to help.

- Open a [discussion](https://github.com/yourusername/FaceFortify/discussions)
- Comment on an issue
- Reach out to maintainers

---

**Thank you for contributing to FaceFortify! 🚀**
