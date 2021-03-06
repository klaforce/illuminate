namespace Illuminate.Interface

open Illuminate.Framework.Types
open Illuminate.Framework.Math
open FSharp.Json
open System.IO
open Illuminate.Interface.Ply
open FsAlg.Generic

module SceneReader =
    let readScene fileName =
        (*for each face in the scene add a triangle to the shape list*)
        let addFace tm result face =
            let face0, face1, face2 = face
            let listVert = Seq.toList result.Vertices
            let v0x, v0y, v0z, n0x, n0y, n0z = listVert.[face0]
            let v1x, v1y, v1z, n1x, n1y, n1z = listVert.[face1]
            let v2x, v2y, v2z, n2x, n2y, n2z = listVert.[face2]

            let N =
                match n0x = 0., n0y = 0., n0z = 0. with
                | true, true, true -> None
                | _ ->
                    let unnormalized =
                        vector [ (n0x + n1y + n2z) / 3.
                                 (n0y + n1y + n1y) / 3.
                                 (n0z + n1z + n2z) / 3. ]

                    Some(unnormalized |> Vector.unitVector)

            let result =
                Triangle
                    { v0 = transformVector (tm.transformations, vector [ v0x; v0y; v0z ])
                      v1 = transformVector (tm.transformations, vector [ v1x; v1y; v1z ])
                      v2 = transformVector (tm.transformations, vector [ v2x; v2y; v2z ])
                      color = tm.color
                      triangleNormal = N
                      transformations = Some tm.transformations }

            result

        let addTriangles tm result =
            let face = addFace tm result
            let faces = Seq.toList result.Faces
            faces |> List.map face

        let parseMesh shape =
            let tm =
                match shape with
                | TriangleMesh m -> m
                | _ ->
                    { filePath = ""
                      triangles = list.Empty
                      color = { r = 0.; g = 0.; b = 0. }
                      transformations = List.empty }

            let parsedFile = parsePLYFile tm.filePath

            match parsedFile with
            | Success p -> addTriangles tm p
            | Failure s -> List.empty<Shape>

        let isMesh shape =
            match shape with
            | TriangleMesh m -> true
            | _ -> false

        let checkForMesh scene = scene.shapes |> List.filter isMesh

        //start scene parsing
        let scene =
            Json.deserialize<Scene> (File.ReadAllText(fileName))

        let meshes = checkForMesh scene

        let triangles =
            List.concat (meshes |> (List.map parseMesh))

        { scene with
              shapes = List.append triangles scene.shapes }
