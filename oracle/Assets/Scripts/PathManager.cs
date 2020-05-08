﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(LineRenderer))]
public class PathManager : MonoBehaviour
{
    [SerializeField]
    private UIManager ui = null;

    private List<string> currentPath = null;
    private LineRenderer currentLine = null;

    public List<string> CurrentPath
    {
        get { return currentPath; }
        set
        {
            currentPath = value;
            if(currentPath == null)
            {
                currentLine.positionCount = 0;
            }
            else
            {
                currentLine.positionCount = currentPath.Count;
                var i = 0;
                foreach(var node in currentPath)
                {
                    var graphComponent = ui.CurrentFloor.GetGraphComponentByID(node);
                    if(graphComponent != null)
                    {
                        currentLine.SetPosition(i, new Vector3(graphComponent.transform.position.x, graphComponent.transform.position.y, 1));
                        i++;
                    }
                }
                currentLine.positionCount = i;
            }
        }
    }

    protected void Awake()
    {
        currentLine = GetComponent<LineRenderer>();
        Assert.IsNotNull(currentLine);
        Assert.IsNotNull(ui);
    }

    public void PathUpdate(List<string> path)
    {
        if(currentPath != null &&
           path.Count > 0 &&
           path[0] == currentPath[0] &&
           !path.SequenceEqual(currentPath))
        {
            CurrentPath = path;
        }
    }
}
