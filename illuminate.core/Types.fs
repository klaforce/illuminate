﻿namespace Illuminate

module Types = 
    (* Framework *)
    type WorldCoordinate = {x: float; y: float; z: float}
    type Direction =  {dirX: float; dirY: float; dirZ: float}
    type Camera = WorldCoordinate
    type ViewPlane = { screenWidth: int; screenHeight: int ; fov: int}
    type Ray = {direction: Direction; origin: WorldCoordinate}
    type Color = { r: float; g: float; b: float }
    type ScreenCoordinate = { i: int; j: int; }
    type Pixel = { coordinate: ScreenCoordinate; color: Color }

    (* Shapes *)
    type Sphere = {origin: WorldCoordinate; radius: float; color: Color}
    type Plane = {origin: WorldCoordinate; width: float; length: float}
    type Shape = 
        | Sphere of Sphere
        | Plane of Plane

    (* Lighting *)
    type PointLight = {origin: WorldCoordinate; luminosity: Color}
    type SpotLight = {origin: WorldCoordinate; luminosity: Color; direction: Direction}
    type Light = 
        | PointLight of PointLight 
        | SpotLight of SpotLight

    (* Scene *)
    type Scene = { shapes: Shape seq; lights: Light seq;}
