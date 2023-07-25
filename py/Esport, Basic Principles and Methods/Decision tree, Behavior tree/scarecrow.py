from wraith import Wraith
import pygame


class Scarecrow(Wraith):
    health = 100        # Current health
    dead = False        # Current death state
    action = 'Idle'     # Current action
    actions = ['Attacking', 'Idle', 'Walking']     # Allowed actions
    default_direction = 1   # Default direction of images
    obj_x = 35      # Offset from left
    obj_y = 32      # Offset from top
    obj_w = 104     # Object width
    obj_h = 120     # Object height

    walk_speed = 6       # Speed of walking
    attack_parts = 30    # Count of attacking frames
    can_hurt = False     # Can be hurt

    def __init__(self, screen_width, screen_height, x, y):
        super().__init__(screen_width, screen_height, x, y, 'scarecrow')
