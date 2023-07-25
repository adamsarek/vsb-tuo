package tools.enumerations;

import tools.Direction;

public enum Direction8{
    NONE     (new Direction(-1)),
    EAST     (new Direction(0)),
    NORTHEAST(new Direction(45)),
    NORTH    (new Direction(90)),
    NORTHWEST(new Direction(135)),
    WEST     (new Direction(180)),
    SOUTHWEST(new Direction(225)),
    SOUTH    (new Direction(270)),
    SOUTHEAST(new Direction(315));

    public final Direction direction;

    private Direction8(Direction direction){
        this.direction = direction;
    }
}

