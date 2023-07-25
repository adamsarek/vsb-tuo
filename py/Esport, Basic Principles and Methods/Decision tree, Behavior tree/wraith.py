import pygame
import numpy as np
from my_object import My_Object


class Wraith(My_Object):
    health = 100        # Current health
    dead = False        # Current death state
    action = 'Idle'     # Current action
    actions = ['Attacking', 'Dying', 'Hurt', 'Idle', 'Walking']     # Allowed actions
    default_direction = 1   # Default direction of images

    walk_speed = 6      # Speed of walking
    attack_parts = 12   # Count of attacking frames
    can_hurt = True     # Can be hurt
    hurt_parts = 12     # Count of hurting frames

    def __init__(self, screen_width, screen_height, x, y, type='enemy_wraith_1'):
        if type == 'enemy_wraith_1':
            self.obj_x = 45      # Offset from left
            self.obj_y = 12      # Offset from top
            self.obj_w = 60      # Object width
            self.obj_h = 88      # Object height
        elif type == 'enemy_wraith_2':
            self.obj_x = 46      # Offset from left
            self.obj_y = 11      # Offset from top
            self.obj_w = 65      # Object width
            self.obj_h = 89      # Object height
        elif type == 'enemy_wraith_3':
            self.obj_x = 42      # Offset from left
            self.obj_y = 9       # Offset from top
            self.obj_w = 67      # Object width
            self.obj_h = 91      # Object height

        super().__init__(screen_width, screen_height, x, y, type)
