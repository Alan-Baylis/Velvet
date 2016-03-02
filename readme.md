# Velvet

A module procedural content generation plugin for Unity. Developed as part of the bachelor thesis *Procedural Generation of Content in Video Games*.

## Prototype

In case you want to see some examples on how to use Velvet, I provided an couple of demos. The demo scenes can be found under *Assets/Prototype/Scenes*.

### spline-shapes

In this scene you will find a game object called **supercylinder_generator** in the hierarchy window. To generate a Supercylinder, just select **supercylinder_generator** and press the *Process* button shown in the inspector view. After the generator is finished a new game object will appear in the hierarchy called **tube**. If you do not see the object in the scene view, select *tube* and click *GameObject->Align View To Selected* in the menu bar.

### vortex-tunnel

The hierarchy of this scene you will find a game object called **vortex_tunnel_generator**. If you want to create a tunnel, just select **vortex_tunnel_generator** and press the *Process* button shown in the inspector view. After the generator is done a new game object called **tower** will appear in the hierarchy. If you do not see the object in the scene view, select **tower** and click *GameObject->Align View To Selected* in the menu bar.

### voxel-landscape

When you open this scene you will find a game object called **terrain_generator** in the hierarchy window. If you want generate a new terrain, just select **terrain_generator** and press the *Process* button shown in the inspector view. After the generator is done a new game object called **map** will have been created. If you do not see the object in the scene view, select **map** and click *GameObject->Align View To Selected* in the menu bar. I advise caution when trying different values in the TerrainGenerator script. A high amount of voxels might slow down the generation process considerably, or could potentially crash Unity.