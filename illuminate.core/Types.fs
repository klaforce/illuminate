﻿namespace Illuminate
open Illuminate.MathTypes

module Types = 
    (* Framework *)
    type WorldCoordinate = {x: float; y: float; z: float}
    type Direction =  {dirX: float; dirY: float; dirZ: float}
    type Camera = WorldCoordinate
    type Ray = {direction: Direction; origin: WorldCoordinate}
    type Color = { r: float; g: float; b: float }
    type ScreenCoordinate = { i: int; j: int; }
    type Pixel = { coordinate: ScreenCoordinate; pixelColor: Color }
    type Image = Pixel list

    (* Shapes *)
    type Sphere = {origin: WorldCoordinate; radius: float; color: Color}
    type Plane = {origin: WorldCoordinate; width: float; length: float; color: Color}
    type Shape = 
        | Sphere of Sphere
        | Plane of Plane

    type HitPoint = {shape: Shape; t: float; point: WorldCoordinate; normal: Normal; shadowOrigin: WorldCoordinate}

    (* Lighting *)
    type PointLight = {pointOrigin: WorldCoordinate; luminosity: Color; intensity: float}
    type DistantLight = {distantLightDirection: Direction; luminosity: Color; intensity: float}
    type SpotLight = {spotOrigin: WorldCoordinate; luminosity: Color; intensity: float; direction: Direction}
    type Light = 
        | PointLight of PointLight
        | DistantLight of DistantLight
        | SpotLight of SpotLight

    type LightHitPoint = {lightDistance: float; lightDirection: Direction; luminosity: Color}
    (* Scene *)
    type Scene = { width: int; height: int; fov: int; shapes: Shape list; lights: Light list; camera: Camera}

    [<Literal>] 
    let infinity = System.Double.MaxValue