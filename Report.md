# Dev Report

This task took around 6-7 hours to complete, plus one hour to write this report.

## Architecture

The game architecture is composed of 3 main components: the map, resource management, and the buildings.

### The map
The `MapManager` contains a grid that is represented as a boolean array with occupied slots in the grid set as `true`. A boolean bi-dimensional array was the quickest solution I found, as the grid size does not change mid-game and we want to quickly access by coordinates.

Building management is kept separate from the map, with the map only being used for collision detection. This just makes both concepts less overlapping and makes it easier to use the map for other components.


### Resource Management
Resource management is done simply with just a dictionary linking resource type and amount inside the `ResourceManager` class. I used enums for resource types and collections to make it easier to add or remove resources.

To allow for easier updating of the UI, I also added a simple binding interface, so when the resources are updated, bounded functions can be called to update the UI. 


### Buildings
Buildings contain most of the game complexity. As most of the functionality between building are very similar, I picked the Residence building and started to build a generic building prefab around this building type. All building types are then just prefab variants of this original prefab.

As we are working with a grid with predefined size (not based on the model), we can also separate the model itself from the component logic itself, so the original prefab only has a placeholder for the model that is then filled in the variants.

The prefab is centered around the `MapBuilding` component which contains 4 main parts, the controls, production, construction and canvas.

The component can be in 3 different states (`Placement`, `Constructing` and `Finished`), which mostly controls what sub-components are active at which time.

#### Controls:
These are the controls used in the game:
- drag to move the building
- click to toggle building UI
- place to construct the building).

I separated the controls as it is easier to activate/deactivate control types depending on game state then to have conditionals on what controls should do what.

#### Production
Building production is handled by the `AutoTimedProduction` component that runs a co-routine to handle the production slider and the expected income at the end. As the only difference between production types is that they are manual or automatic, this can simply be done with a flag to check if the co-routine should restart after giving income.

Production parameters are set in this component through `ProductionPerSecond` and `Resource`. In hindsight, it would be best for production parameters to be a resource amount array to allow for multi-resource production.

To allow for decorations the production component is optional.

#### Construction

Construction is similar to production as it's just a co-routine that handles the construction slider and changes the building state at the end, but it also handles checking and paying for resource costs.

#### Canvas
To achieve the UI that appears on top of the building for the sliders and building name I used a world space canvas to make it easier to keep it aligned with the building. I also added a simple script to keep the canvas directed towards the camera. UI for each state is just activated and deactivated the building state changes.


## UI 
Didn't have much time so I just added a component to the main `Canvas` that controls which component are active for a certain state. This just makes it quicker to deactivate and active what I needed for each game state.


## Things I missed
- Forgot to add cost to the build building buttons. To keep the building costs centralized I could fetch the cost data from the prefab.
- I also would like to have more UI things auto-filled with already defined data. An example of this is the build building button that could have their button text automatically filled with the building name when the building type in the button changes.
- Data bindings can also be generalized into a library instead of being implemented on-site.