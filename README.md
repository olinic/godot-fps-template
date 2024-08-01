# FPS Game

## Setup

Create a `GODOT_BIN` environment variable and point it to the Godot executable.


## Programming Practices

### Exports

- Use `[Export]` for Nodes. Avoid exporting properties.
- Use `PascalCase` for naming exports.

> Following these practices will limit breaking changes in the future and make refactoring easier for properties.

Do:
```cs
[Export] public Node3D Camera;
public int Height = 10;
```

Don't:
```cs
[Export] private Node3D _Camera; // Should be "Camera"
[Export] public int Height = 10; // Avoid exporting properties
```

> Remember to update scenes if you are updating the name of an export.