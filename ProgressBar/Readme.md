Basic progress bar components (liniear and radial).

#### Usage
For a linear progress bar
- Given an UI Gameobject with Image component
- add the ProgressBar script and an implementation of the AbstractProgressProvider (see Scripts/Example/BombTimeProvider.cs together with the Bomb prefab) to the GameObject
- add an additional Panel with Image component as child to the GameObject
  - Rect Transform settings
    - stretch in X and Y
    - Anchors: Min X=0 Y=0, Max X=1 Y=1
    - Pivot: X=0 Y=0
  - link this Panel to the variable BarTransform of the ProgressBar script

For a radial progress bar
- Given an UI Gameobject with Image component
  - Image settings
    - Image Type: Filled
    - Fill Method: Radial 360
    - Fill Origin: Top
    - Clockwise: false
- add the RadialProgressBar script and an implementation of the AbstractProgressProvider (see Scripts/Example/BombTimeProvider.cs together with the Bomb prefab) to the GameObject
