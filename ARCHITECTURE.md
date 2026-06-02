# ARCHITECTURE OVERVIEW

## 1. Inventory Grid System (Multi-Cell Inventory)

### Overview

The inventory system is implemented as a 2D grid where each item can occupy multiple cells, similar to inventory systems found in games such as Resident Evil and Backpack Hero.

### Core Components

#### InventoryGrid

Responsible for managing all inventory data:

- Grid dimensions (Width, Height)
- Item collection
- Placement validation
- Item placement and removal
- Cell occupancy tracking
- Item lookup by cell position
- Collision detection

#### ItemInstance

Represents an item currently placed in the inventory:

- Reference to ItemDefinition
- Origin position
- Rotation state
- Occupied cells
- Current shape

#### InventoryGridUI

Responsible for rendering the inventory:

- Creates grid cells
- Creates item views
- Synchronizes UI with inventory data
- Refreshes when inventory changes

#### InventoryItemView

Visual representation of an item:

- Item icon
- Dynamic size based on shape
- Drag & Drop support
- Rotation visualization

### Placement Validation

Whenever an item is placed:

1. Verify that all occupied cells are within grid bounds.
2. Verify that no occupied cells overlap another item.
3. If valid:
   - Remove previous occupancy data.
   - Update occupied cells.
   - Update origin position.
   - Refresh UI.

---

## 2. Data-Driven System

### Overview

All gameplay data is managed through ScriptableObjects, ensuring a clean separation between data and logic.

### ItemDefinition

Contains:

- Item ID
- Display Name
- Icon
- Shape
- Rotatable Flag

### RecipeDefinition

Contains:

- InputAId
- InputBId
- OutputItemId

### LevelGoalDefinition

Contains:

- Goal list
- Target item
- Required amount

### Benefits

- Easy content creation
- No hard-coded gameplay data
- Addressables-ready
- Supports remote content updates

---

## 3. Drag & Drop System

### Overview

Drag & Drop is implemented using Unity's Event System interfaces.

### Flow

#### Begin Drag

- Store original position
- Create placement preview

#### Dragging

- Track cursor position
- Convert screen position to grid coordinates
- Display valid or invalid placement preview

#### End Drag

- Validate drop position
- If valid:
  - Place item

- If invalid:
  - Return item to original position

### Validation Rules

Items cannot:

- Be placed outside the grid
- Overlap existing items
- Overlap spawned loot

---

## 4. Item Rotation System

### Overview

Items support 90-degree rotation.

### Rotation States

- Normal
- Rotated

### Shape Transformation

Example:

Original Shape:

(0,0)
(1,0)
(2,0)

Rotated Shape:

(0,0)
(0,1)
(0,2)

### Rotation Validation

Before rotating:

1. Calculate rotated shape.
2. Check collisions.
3. Check grid boundaries.

Rotation is applied only if all validations pass.

### Visual Update

InventoryItemView updates:

- Icon rotation
- UI size
- Occupied cells

---

## 5. Crafting System

### Overview

Crafting occurs when one item is dragged directly onto another item.

### Core Component

#### CraftingService

Responsible for:

- Managing recipes
- Detecting valid crafting combinations
- Returning crafting results

### Crafting Flow

1. Drag Item A.
2. Drop Item A onto Item B.
3. Validate recipe.
4. If valid:
   - Remove Item A.
   - Remove Item B.
   - Create crafted item.

5. Refresh inventory UI.

---

## 6. Recipe Detection System

### Overview

Recipe matching is based on Item IDs rather than ScriptableObject references.

### Example

Wood + Stone

→ Axe

### Order Independence

Supports:

Wood + Stone

and

Stone + Wood

Both produce the same result.

### Benefits

- Independent of object references
- Compatible with remote content updates
- Save/Load friendly

---

## 7. Level Goal System

### Overview

Tracks player progress toward level objectives.

### GoalTracker

Maintains progress for all active goals.

### GoalProgress

Stores:

- Goal definition
- Required amount
- Current amount

### Goal Update Flow

Whenever crafting succeeds:

1. Identify crafted item.
2. Update matching goals.
3. Refresh goal UI.

### Completion

When all goals are completed:

LevelCompleted = true

---

## 8. Level Transition System

### Overview

Handles progression between levels.

### Flow

1. All goals completed.
2. Display Level Complete popup.
3. Wait for player confirmation.
4. Load next level.

### Scene Flow

Level 1

↓

Level Complete

↓

Level 2

↓

Level Complete

↓

Level 3

---

## 9. Loot Spawning System

### Overview

Loot is spawned through a ScrollView containing loot buttons.

### Loot Database

Each button contains:

- Item ID
- Icon

### Spawn Flow

1. Player clicks a loot button.
2. System finds a valid empty position.
3. Item is spawned into inventory.
4. UI is refreshed.

### Validation

Loot will not spawn if:

- No valid placement exists
- Inventory is full

---

## 10. Addressables Remote Content

### Overview

Gameplay content can be downloaded remotely using Unity Addressables.

### Remote Assets

- ItemDefinition
- RecipeDefinition
- Sprites
- Prefabs

### Architecture

GitHub Pages

↓

Catalog

↓

Asset Bundles

↓

Client Download

### Runtime Flow

1. Check for catalog updates.
2. Download dependencies.
3. Load assets.
4. Cache content locally.

### Benefits

- No application rebuild required for content updates
- Only bundles and catalogs need to be uploaded
- Supports live content delivery

---

## 12. Unit Testing

### Framework

Unity Test Framework

### Test Categories

#### Inventory Tests

- Place Item
- Remove Item
- Overlap Validation
- Boundary Validation

#### Rotation Tests

- Shape Rotation
- Collision Validation
- Boundary Validation

#### Crafting Tests

- Valid Recipe
- Invalid Recipe
- Order Independent Recipes

#### Goal Tests

- Goal Progress Tracking
- Goal Completion

### Purpose

Ensures:

- Stable gameplay logic
- Regression prevention
- Easier maintenance
- Safe future expansion

---

# Technical Stack

- Unity 6
- C#
- ScriptableObject
- Addressables
- UniTask
- DOTween
- Unity Test Framework

# Design Principles

- Data-Driven Architecture
- Single Responsibility Principle
- Separation of Data and Logic
- Modular Gameplay Systems
- Remote Content Ready
- Unit Test Friendly
- Scalable and Maintainable

---

# NOTE

In practice, a robust and well-designed system architecture typically requires at least 6 to 12 months of design, implementation, refinement, and testing before it is ready for production use. However, within the 7-day deadline for the test, I did my best to utilize my available free time to complete the implementation.
At its current stage, I recognize that the architecture is far from perfect and still requires significant improvements and further refinement. With more time, I would have been able to build a more scalable and maintainable system that fully embraces Object-Oriented Programming (OOP) principles, SOLID principles, and proven Design Patterns—areas that are only partially implemented or not yet fully represented in the current solution.

Thank you !!!
