namespace AstronomicalObjects;

public class Meteor:Meteoroid
{
    public override string Do() => base.Do() + "Откуда этот запах горелого? ";
}