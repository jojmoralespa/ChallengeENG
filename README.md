# Revit Parameter Scanner

# Setup instructions
if you compile the solution, the addin automatically will be installed inside your folder of addins ( in this case will be installed in 2023 version, there is one version for 2020 in another branch of this repository).

if your're using Visual Studio 2022 , you can compile your solution with:

```bash
  Ctrl + B
```

## How it works
This add-in loads parameters only if your current view is valid (it must be a 3D View, Floor Plan, or Reflected Ceiling Plan).

Once you're in a valid view and press "Load Parameters," the parameters within the elements will be listed in a ComboBox with search functionality. After selecting a parameter, the corresponding parameter values will appear in another ComboBox.

Select a parameter value, and you’ll be able to either isolate the elements in the view or select them.
