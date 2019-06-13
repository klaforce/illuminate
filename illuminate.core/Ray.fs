namespace Illuminate
open Illuminate.Types
open Illuminate.Operators
open Illuminate.Math
open Illuminate.Material
open Illuminate.Hit

module Ray = 
    let castRay (ray:Direction,pixel:ScreenCoordinate) (scene:Scene) =
        (*
            Stupid debugging needed
        
        if pixel.i = 336 && pixel.j = 206
        then 
            printf ""
        *)
        
        let hit = getHitPoint ray scene
        let hitColor = 
            match hit with
                | Some point -> getHitColor (point, scene)
                | None -> {r = 0.; g = 0.; b = 0.}

        {coordinate = pixel; color = hitColor}