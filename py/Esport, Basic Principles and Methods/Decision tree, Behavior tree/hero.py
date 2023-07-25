import pygame
from my_object import My_Object


class Hero(My_Object):
    health = 100        # Current health
    dead = False        # Current death state
    action = 'Idle'     # Current action
    actions = ['Attacking', 'Dying', 'Hurt', 'Idle', 'Walking']     # Allowed actions
    default_direction = 0   # Default direction of images
    obj_x = 45      # Offset from left
    obj_y = 32      # Offset from top
    obj_w = 66      # Object width
    obj_h = 91      # Object height

    walk_speed = 8      # Speed of walking
    attack_parts = 15   # Count of attacking frames
    can_hurt = True     # Can be hurt
    hurt_parts = 15     # Count of hurting frames

    def __init__(self, screen_width, screen_height, x, y):
        super().__init__(screen_width, screen_height, x, y, 'hero')
