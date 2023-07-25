package com.example.snake;

import java.util.Arrays;
import java.util.Random;

public class Snake {
    private final int gameWidth;
    private final int gameHeight;

    // Directions (0 = up, 1 = right, 2 = down, 3 = left)
    private final int[] dirX = new int[]{0, 1, 0, -1};
    private final int[] dirY = new int[]{-1, 0, 1, 0};

    // Snake
    private int dir;
    private int len = 3;
    private Block[] blocks;
    private Block prevTailBlock;

    public Snake(int gameWidth, int gameHeight) {
        this.gameWidth = gameWidth;
        this.gameHeight = gameHeight;

        Random random = new Random();
        this.dir = random.nextInt(4);

        blocks = new Block[len];
        int x = random.nextInt(gameWidth - len * 2);
        int y = random.nextInt(gameHeight - len * 2);
        for(int i = 0; i < len; i++) {
            blocks[i] = new Block(x + len, y + len);
            x -= dirX[dir];
            y -= dirY[dir];
        }
    }

    public Block[] getBlocks() {
        return blocks;
    }

    public boolean changeDirection(int dir) {
        if(this.dir % 2 != dir % 2) {
            this.dir = dir;
            return true;
        }
        return false;
    }

    public boolean move() {
        prevTailBlock = new Block(blocks[blocks.length - 1].getX(), blocks[blocks.length - 1].getY());

        int x = blocks[0].getX() + dirX[dir];
        int y = blocks[0].getY() + dirY[dir];

        for(int i = blocks.length - 1; i >= 1; i--) {
            blocks[i] = blocks[i-1];

            if(x == blocks[i].getX() && y == blocks[i].getY()) {
                return false;
            }
        }
        blocks[0] = new Block((x >= gameWidth ? 0 : (x < 0 ? gameWidth - 1 : x)), (y >= gameHeight ? 0 : (y < 0 ? gameHeight - 1 : y)));

        return true;
    }

    public void eat() {
        len++;

        blocks = Arrays.copyOf(blocks, len);
        blocks[len-1] = prevTailBlock;
    }
}
