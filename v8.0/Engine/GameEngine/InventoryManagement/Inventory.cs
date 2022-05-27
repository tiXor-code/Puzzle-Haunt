using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Engine
{
    public class Inventory
    {
        #region Fields

        public List<IEntity> ItemsList = new List<IEntity>();

        public int _inventoryScore = 0;

        #endregion Fields

        #region Properties

        public int InventoryScore
        { get { return _inventoryScore; } set { _inventoryScore = value; } }

        #endregion Properties

        #region Methods

        public Inventory()
        {
        }

        public void InventoryUI()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            while (keyboardState.IsKeyDown(Keys.Tab))
            {
            }
        }

        #endregion Methods
    }
}