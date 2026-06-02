# OPTIMIZATION STRATEGY

## Overview

This project was designed with scalability, maintainability, and runtime performance in mind. Several optimization techniques were applied to minimize memory usage, reduce unnecessary allocations, and improve overall system responsiveness.

---

# 1. Data-Driven Architecture

## Optimization Goal

Reduce code complexity and improve maintainability.

## Implementation

Gameplay content is stored in ScriptableObjects:

- ItemDefinition
- RecipeDefinition
- LevelGoalDefinition

Benefits:

- No hard-coded gameplay values.
- Faster iteration for designers.
- Easy content updates through Addressables.
- Lower maintenance cost.

---

# 2. ID-Based Gameplay Logic

## Optimization Goal

Reduce dependency on object references.

## Implementation

Gameplay systems use unique IDs instead of ScriptableObject reference comparisons.

Example:

```csharp
item.Definition.Id == recipe.InputAId
```

instead of:

```csharp
item.Definition == recipe.InputA
```

Benefits:

- More reliable with Addressables.
- Supports remote content updates.
- Simplifies Save/Load implementation.
- Prevents reference mismatch issues.

---

# 3. Inventory Occupancy Tracking

## Optimization Goal

Fast collision and placement validation.

## Implementation

Each ItemInstance maintains a list of occupied cells.

Example:

```csharp
OccupiedCells
```

Benefits:

- Faster overlap detection.
- Avoids recalculating item shape repeatedly.
- Simplifies placement validation.

---

# 4. Object Pooling

## Optimization Goal

Reduce runtime allocations and garbage collection spikes.

## Implementation

Reusable UI elements can be pooled:

- InventoryItemView
- Loot Buttons
- Goal UI Entries

Benefits:

- Reduced Instantiate/Destroy calls.
- Improved UI performance.
- Lower GC pressure.

---

# 5. Addressables Asset Management

## Optimization Goal

Reduce application size and improve content delivery.

## Implementation

Remote Addressables are used for:

- Item Definitions
- Recipe Definitions
- Sprites
- Future gameplay content

Benefits:

- Smaller initial build size.
- Dynamic content updates.
- Faster deployment pipeline.
- Better memory management through on-demand loading.

---

# 6. Asynchronous Loading with UniTask

## Optimization Goal

Avoid blocking the main thread.

## Implementation

Asset loading and downloading are performed asynchronously.

Example:

```csharp
await Addressables.LoadAssetAsync<T>();
```

Benefits:

- Smooth user experience.
- Non-blocking asset loading.
- Reduced frame drops.

---

# 7. Catalog Update System

## Optimization Goal

Avoid unnecessary downloads.

## Implementation

Before downloading remote content:

1. Check catalog updates.
2. Compare remote catalog hash.
3. Download only changed bundles.

Benefits:

- Lower bandwidth usage.
- Faster startup time.
- Smaller patch sizes.

---

# 8. Download Size Validation

## Optimization Goal

Prevent redundant downloads.

## Implementation

Before downloading:

```csharp
Addressables.GetDownloadSizeAsync()
```

Benefits:

- Downloads only required content.
- Reduces network usage.
- Improves loading efficiency.

---

# 9. Efficient Crafting Detection

## Optimization Goal

Minimize recipe lookup cost.

## Current Implementation

Recipes are checked through a centralized CraftingService.

Benefits:

- Single responsibility.
- Easy maintenance.
- Consistent recipe validation.

## Future Improvement

For larger recipe collections:

```csharp
Dictionary<string, RecipeDefinition>
```

using a composite key:

```text
stone_wood
```

Benefits:

- O(1) recipe lookup.
- Faster crafting validation.

---

# 10. Rotation Validation Optimization

## Optimization Goal

Avoid invalid inventory updates.

## Implementation

Before applying rotation:

1. Calculate rotated shape.
2. Validate boundaries.
3. Validate collisions.

Only apply rotation if all checks succeed.

Benefits:

- Prevents unnecessary state changes.
- Reduces UI refresh operations.

---

# 11. UI Refresh Optimization

## Optimization Goal

Avoid rebuilding the entire inventory UI.

## Current Strategy

Refresh only affected items after:

- Placement
- Removal
- Rotation
- Crafting

Benefits:

- Reduced UI updates.
- Better scalability for larger inventories.

---

# 12. Unit Testing

## Optimization Goal

Reduce debugging and maintenance time.

## Covered Systems

- Inventory Grid
- Item Placement
- Item Rotation
- Crafting
- Goal Tracking

Benefits:

- Prevents regressions.
- Improves code reliability.
- Enables safer refactoring.

---

# Memory Optimization

The project minimizes runtime memory usage through:

- ScriptableObject-based data storage.
- Addressables on-demand loading.
- Optional object pooling.
- Shared asset references.
- Cached inventory occupancy data.

---

# Scalability Considerations

The architecture is designed to support future features with minimal refactoring:

- Additional item types.
- More crafting recipes.
- Equipment systems.
- Quest systems.
- Remote live content updates.
- Cloud save integration.
- Multiplayer inventory synchronization.

---

# Summary

The project prioritizes:

- Data-driven design
- Runtime efficiency
- Modular architecture
- Remote content support
- Low memory overhead
- Easy maintenance
- Future scalability

## These optimizations ensure the project remains performant, maintainable, and ready for future feature expansion.

---

# NOTE

I decided not to use the Unity Profiler for this test, because I didn't have enough time to complete this test within the 7-day deadline. However, I believe that if the system architecture is designed correctly from the beginning, performance optimization becomes almost unnecessary
Do you agree with this perspective?

Thank you !!!
