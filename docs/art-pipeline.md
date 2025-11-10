# Art Pipeline Documentation

Guide for creating and managing art assets for INKBLADE: ONE BULLET SAMURAI.

## 🎨 Visual Style

**Aesthetic:** Black & White Sumi-e Ink Art
- High contrast silhouettes
- Minimal color palette (black, white, subtle grays)
- Brush-stroke effects
- Rice-paper texture backgrounds

## 🛠️ Tools Required

### Essential
- **Inkscape** - Vector graphics (silhouettes, shapes)
- **Krita** - Raster painting (brush effects, textures)
- **GIMP** - Image editing and optimization

### Optional
- **Unity** - Sprite import and animation
- **Aseprite** - Pixel art (if using pixel style)

## 📐 Asset Specifications

### Sprites

**Player:**
- Size: 64x64 pixels (base)
- Format: PNG with transparency
- Color: Black silhouette on transparent background
- Export: Multiple sizes (32x32, 64x64, 128x128) for different zoom levels

**Enemies:**
- Size: 48x48 to 96x96 pixels (varies by enemy type)
- Format: PNG with transparency
- Style: Black silhouettes, distinct shapes

**Bullet:**
- Size: 16x16 pixels
- Format: PNG with transparency
- Style: Simple black circle or ink drop shape

**UI Elements:**
- Size: Varies (buttons, icons, panels)
- Format: PNG with transparency
- Style: Minimal, clean lines

### Backgrounds

**Level Backgrounds:**
- Size: 1920x1080 (or target resolution)
- Format: PNG or JPG
- Style: Low-contrast rice-paper texture
- Color: Very light gray/beige with subtle noise

**Parallax Layers:**
- Multiple layers for depth
- Foreground: Darker, more detailed
- Background: Lighter, blurred

## 🎨 Creation Process

### 1. Character Silhouettes (Inkscape)

**Steps:**
1. Open Inkscape
2. Create new document (64x64 or target size)
3. Draw simple black shapes using:
   - Rectangle tool
   - Circle tool
   - Path tool (for custom shapes)
4. Combine shapes using Path > Union
5. Export as PNG:
   - File > Export PNG Image
   - Set size (64x64, 128x128, etc.)
   - Enable transparency

**Tips:**
- Keep shapes simple and recognizable
- Use negative space effectively
- Test at small sizes for readability

### 2. Brush Effects (Krita)

**Steps:**
1. Open Krita
2. Create new canvas (256x256 or larger)
3. Use ink brush presets or download Sumi-e brushes
4. Paint brush strokes on transparent background
5. Export as PNG with transparency

**Brush Sources:**
- Krita default brushes
- Free brush packs (search "Sumi-e brushes Krita")
- Create custom brushes from scanned ink strokes

### 3. Background Textures

**Rice Paper Texture:**
1. Create base in Krita or GIMP
2. Apply noise filter (subtle)
3. Add slight blur
4. Adjust contrast (very low)
5. Export as JPG (smaller file size)

**Alternative:**
- Use free texture resources
- Apply filters to match aesthetic
- Ensure low contrast

### 4. Particle Effects

**Ink Splash:**
1. Create in Krita or GIMP
2. Use brush tool with ink-like brush
3. Create multiple frames for animation
4. Export as sprite sheet

**Simple Approach:**
- Use Unity's Particle System
- Create simple black sprites
- Configure in Unity Editor

## 📁 Asset Organization

```
Assets/Art/
├── Characters/
│   ├── Player/
│   │   ├── player_idle.png
│   │   ├── player_run.png
│   │   └── player_dash.png
│   └── Enemies/
│       ├── enemy_basic.png
│       └── enemy_fast.png
├── Weapons/
│   └── bullet.png
├── Effects/
│   ├── ink_splash.png
│   ├── dash_trail.png
│   └── hit_particle.png
├── UI/
│   ├── button.png
│   ├── panel.png
│   └── icons/
├── Backgrounds/
│   ├── bg_layer_0.png (far)
│   ├── bg_layer_1.png (mid)
│   └── bg_layer_2.png (near)
└── Textures/
    └── rice_paper.jpg
```

## 🎬 Animation

### Sprite Sheet Animation

1. Create frames in Krita/GIMP
2. Export as sprite sheet (horizontal or vertical)
3. Import to Unity
4. Configure Sprite Editor:
   - Set Sprite Mode to "Multiple"
   - Slice sprites
   - Set Pixels Per Unit (typically 100)

### Unity Animator

1. Create Animator Controller
2. Create Animation Clips from sprite sheets
3. Set up state machine
4. Configure transitions

**Example Animation Setup:**
```
Player Animator:
  - Idle (loop)
  - Run (loop)
  - Dash (single)
  - Death (single)
```

## 🖼️ Import Settings (Unity)

### Sprite Import Settings

1. Select sprite in Project window
2. Inspector > Texture Type: **Sprite (2D and UI)**
3. Configure:
   - **Pixels Per Unit:** 100 (standard)
   - **Filter Mode:** Bilinear (smooth) or Point (pixel art)
   - **Compression:** None (for crisp edges) or High Quality
   - **Max Size:** Match your target resolution

### Sprite Atlas (Performance)

1. Create Sprite Atlas:
   - Right-click in Project > Create > Sprite Atlas
2. Add sprites to atlas
3. Configure:
   - **Format:** RGBA 32 bit (for quality)
   - **Compression:** None or High Quality

**Benefits:**
- Reduces draw calls
- Improves performance
- Better batching

## 🎨 Color Palette

**Primary Colors:**
- Black: `#000000` (RGB: 0, 0, 0)
- White: `#FFFFFF` (RGB: 255, 255, 255)
- Light Gray: `#F5F5F5` (RGB: 245, 245, 245) - Backgrounds
- Dark Gray: `#1A1A1A` (RGB: 26, 26, 26) - Shadows

**Accent (Optional, minimal use):**
- Red: `#FF0000` (RGB: 255, 0, 0) - Damage indicators only

## 📏 Resolution Guidelines

**Target Resolutions:**
- Desktop: 1920x1080
- WebGL: 1280x720 (for performance)
- Mobile: 1080x1920 (portrait) or 1920x1080 (landscape)

**Asset Sizes:**
- UI: Match target resolution
- Sprites: Scale appropriately (1x, 2x, 4x for retina)
- Backgrounds: Match or exceed target resolution

## 🔄 Workflow

1. **Design** - Sketch or plan in Inkscape/Krita
2. **Create** - Draw/paint assets
3. **Export** - Save as PNG/JPG
4. **Import** - Add to Unity project
5. **Configure** - Set import settings
6. **Test** - Verify in game
7. **Optimize** - Compress if needed, create atlases

## 📚 Resources

### Free Asset Sources
- [Kenney.nl](https://kenney.nl/) - Free game assets
- [OpenGameArt.org](https://opengameart.org/) - Community assets
- [Itch.io Free Assets](https://itch.io/game-assets/free) - Various free assets

### Brush Resources
- [Krita Brush Presets](https://krita.org/en/learn/tutorials/)
- Search "Sumi-e brush Krita" for free brush packs

### Texture Resources
- [Textures.com](https://www.textures.com/) - Free textures (with account)
- [CC0 Textures](https://cc0textures.com/) - Public domain textures

## ✅ Quality Checklist

Before finalizing assets:
- [ ] Transparent backgrounds where needed
- [ ] Correct pixel dimensions
- [ ] Consistent art style
- [ ] Readable at small sizes
- [ ] Optimized file sizes
- [ ] Proper import settings in Unity
- [ ] Tested in-game at target resolution

## 🎯 Tips

1. **Start Simple:** Begin with basic shapes, add detail later
2. **Test Early:** Import to Unity frequently to see how it looks
3. **Consistency:** Maintain consistent line weights and styles
4. **Performance:** Use sprite atlases for better performance
5. **Backup:** Keep source files (SVG, KRA, XCF) for future edits

