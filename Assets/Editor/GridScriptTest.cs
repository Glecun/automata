using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridScriptTest
    {
        [Test]
        public void should_create_grid()
        {
            GridScript gridScript = new GridScript(1,2,3);
            Assert.AreEqual(gridScript.GetCellSize(), 1);
            Assert.AreEqual(gridScript.GetX(), 2);
            Assert.AreEqual(gridScript.GetY(), 3);
        }

        [Test]
        public void should_get_xy_with_world_position()
        {
            GridScript gridScript = new GridScript(10, 4, 4);
            int x, y;
            var worldPosition = new Vector3(32, 32);
            gridScript.GetXY(worldPosition, out x, out y);

            Assert.AreEqual(x, 3);
            Assert.AreEqual(y, 3);
           
        }

        [Test]
        public void should_get_world_position_with_xy()
        {
            GridScript gridScript = new GridScript(10, 4, 4);
            
            var worldPosition = gridScript.GetWorldPosition(2,3);

            var expectedWorldPosition = new Vector3(20, 30);

            Assert.AreEqual(worldPosition, expectedWorldPosition);
        }
    }
}
