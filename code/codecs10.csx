using System;

public enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public enum AttackType
{
    Light = 0,
    Normal = 1,
    Heavy = 2,
    Special = 3
}

public class CharacterData
{
    public byte ID { get; set; }
    public byte Health { get; set; }
    public byte Energy { get; set; }
    public bool Moving { get; set; }
    public bool Jumping { get; set; }
    public bool Sprinting { get; set; }
    public bool Attacking { get; set; }
    public Direction Direction { get; set; }
    public AttackType AttackType { get; set; }
    public short X { get; set; }
    public short Y { get; set; }

    public Int64 Save ()
    {
        var id = Convert.ToString(ID, 2).PadLeft(8, '0');
        var health = Convert.ToString(Health, 2).PadLeft(8, '0');
        var energy = Convert.ToString(Energy, 2).PadLeft(8, '0');
        var moving = BoolToString(Moving);
        var jumping = BoolToString(Jumping);
        var sprinting = BoolToString(Sprinting);
        var attacking = BoolToString(Attacking);
        var direction = DirectionToString(Direction);
        var attackType = AttackTypeToString(AttackType);
        var x = Convert.ToString(X, 2).PadLeft(16, '0');
        var y = Convert.ToString(Y, 2).PadLeft(16, '0');

        var savedString = id + health + energy + moving + jumping + sprinting + attacking + direction + attackType + x + y;
        return Convert.ToInt64(savedString, 2);
    }

    public void Load (Int64 value)
    {
        var loadedString = Convert.ToString(value, 2).PadLeft(64, '0');
        
        ID = Convert.ToByte(loadedString.Substring(0, 8), 2);
        Health = Convert.ToByte(loadedString.Substring(8, 8), 2);
        Energy = Convert.ToByte(loadedString.Substring(16, 8), 2);
        Moving = StringToBool(loadedString.Substring(24, 1));
        Jumping = StringToBool(loadedString.Substring(25, 1));
        Sprinting = StringToBool(loadedString.Substring(26, 1));
        Attacking = StringToBool(loadedString.Substring(27, 1));
        Direction = StringToDirection(loadedString.Substring(28, 2));
        AttackType = StringToAttackType(loadedString.Substring(30, 2));
        X = Convert.ToInt16(loadedString.Substring(32, 16), 2);
        Y = Convert.ToInt16(loadedString.Substring(48, 16), 2);
    }

    private string BoolToString(bool value) => value ? "1" : "0";

    private bool StringToBool(string value) => value == "1" ? true : false;

    private string DirectionToString(Direction value)
    {
        switch(value)
        {
            case Direction.Up: return "00";
            case Direction.Down: return "01";
            case Direction.Left: return "10";
            case Direction.Right: return "11";
        }
        return null;
    }

    private Direction StringToDirection(String value)
    {
        switch(value)
        {
            case "00": return Direction.Up;
            case "01": return Direction.Down;
            case "10": return Direction.Left;
            case "11": return Direction.Right;
        }

        throw new ArgumentException("Not a valid value!");
    }

    private string AttackTypeToString(AttackType value)
    {
        switch(value)
        {
            case AttackType.Light: return "00";
            case AttackType.Normal: return "01";
            case AttackType.Heavy: return "10";
            case AttackType.Special: return "11";
        }
        return null;
    }

    private AttackType StringToAttackType(string value)
    {
        switch(value)
        {
            case "00": return AttackType.Light;
            case "01": return AttackType.Normal;
            case "10": return AttackType.Heavy;
            case "11": return AttackType.Special;
        }
        
        throw new ArgumentException("Not a valid value!");
    }
}