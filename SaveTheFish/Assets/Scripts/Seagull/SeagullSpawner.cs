﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Seagull
{
    public class Path
    {
        const float RESOLUTION = 100;

        Spawner p1;
        Spawner p2;

        public Path(Spawner p1, Spawner p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public void DrawGizmo()
        {
            Bezier bezier = Bezier.CubicBezier(p1.origin, p1.end, p2.back, p2.origin);
            float delta = 1 / RESOLUTION;
            for(var i = 0; i < RESOLUTION; i++)
            {
                Gizmos.color = Color.Lerp(p1.color, p2.color, i * delta);
                Gizmos.DrawLine(bezier.Calculate(i * delta), bezier.Calculate((i + 1) * delta));
            }
        }
    }

    public class SeagullSpawner : MonoBehaviour
    {
        [SerializeField]
        private bool dawGizmoWhenNotSelected;

        public void OnDrawGizmos()
        {
            if(dawGizmoWhenNotSelected)
            {
                OnDrawGizmosSelected();
            }
        }
        public void OnDrawGizmosSelected()
        {
            List<Path> paths = GeneratePaths();
            foreach(var path in paths)
            {
                path.DrawGizmo();
            }
        }

        private List<Path> GeneratePaths()
        {
            List<Path> paths = new List<Path>();
            foreach (var startingPoint in FindSpawnPoints("start"))
            {
                foreach (var landingPoint in FindSpawnPoints("landing"))
                {
                    paths.Add(new Path(startingPoint, landingPoint));
                }
            }
            foreach (var landingPoint in FindSpawnPoints("landing"))
            {
                foreach (var endPoint in FindSpawnPoints("end"))
                {
                    paths.Add(new Path(landingPoint, endPoint));
                }
            }
            return paths;
        }

        private IEnumerable<Spawner> FindSpawnPoints(string tag)
        {
            var objects = GameObject.FindGameObjectsWithTag("Respawn");
            return objects
                .Select(x => x.GetComponent<Spawner>())
                .Where(x => x != null && x.spawnTag == tag);
        }
    }

}