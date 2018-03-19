public enum ComponentType
{
    Sprite,
    PlayerInput,
    Animation,
	Collision,
	AIMovement,
	AnimationNPC,
	Camera,
	GUI,
	Damage,
	Stats,
	Inventory
}

public enum Input
{
    Left,
    Right,
    Up,
    Down,
    None,
	Enter,
	LeftClick,
	RightClick
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down,
	NULL
}

public enum State
{
    Standing,
    Walking
}

public enum ItemSlot
{
	slot1,
	slot2,
	slot3,
	slot4
}

public enum Objects
{
	TriggerScene,
	Sign,
	Enemy
}

public enum Scene
{
	Shop,
	Dungeon,
	Town
}