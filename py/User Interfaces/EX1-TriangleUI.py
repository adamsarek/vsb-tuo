# -*- coding: utf-8 -*-

from tkinter import *
from math import sqrt


class MyApplication:
    GENERAL_TRIANGLE = "Obecný trojúhelník"
    RIGHT_TRIANGLE = "Pravoúhlý trojúhelník"
    EQUILATERAL_TRIANGLE = "Rovnostranný trojúhelník"

    def __init__(self, root):
        self._root = root
        self._root.title("Triangle")
        
        # create Frame with Pack container
        self._top = Frame(self._root)
        self._top.pack(expand = TRUE, fill = BOTH
        )
        
        # TODO top
        # @header
        self._header = Frame(self._top, relief = GROOVE, borderwidth = 2)
        self._header.pack(fill = X, padx = 4, pady = 4)
        
        # @header_label
        self._header_label = Label(self._header, text = "Obvod a obsah trojúhelníku")
        self._header_label.pack(fill = BOTH, padx = 8, pady = 8)
        
        # inputs area
        self._left_frame = Frame(
            self._top, 
            relief = GROOVE, # FLAT, RAISED, SUNKEN, GROOVE, RIDGE
            borderwidth = 2, 
        )
        self._left_frame.pack(
            fill = Y, side=LEFT, 
            padx = 4, pady = 4, ipady = 4
        )
        
        # labels for inputs. TODO b and c
        self._label_a = Label(self._left_frame, text = "Strana a =")
        self._label_b = Label(self._left_frame, text = "Strana b =")
        self._label_c = Label(self._left_frame, text = "Strana c =")
        
        # inputs
        self._input_a = Entry(self._left_frame, width = 14)
        self._input_b = Entry(self._left_frame, width = 14)
        self._input_c = Entry(self._left_frame, width = 14)
        
        # assign labels and inputs
        self._label_a.pack(fill=X, padx = 8, pady = 1)
        self._input_a.pack(padx = 8, pady = 1)
        self._label_b.pack(fill=X, padx = 8, pady = 1)
        self._input_b.pack(padx = 8, pady = 1)
        self._label_c.pack(fill=X, padx = 8, pady = 1)
        self._input_c.pack(padx = 8, pady = 1)
        
        # button
        self._button = Button(
            self._left_frame, 
            text = "Vymazat", 
            command = self.reset # command without brackets
            # command = lambda: self.reset() # alternative
        )
        self._button.pack(padx = 4, pady = 4)
        
        self.reset()
        
        # TODO area with computed results
        # @right_frame
        self._right_frame = Frame(self._top)
        self._right_frame.pack(expand = TRUE, fill = BOTH, side = RIGHT, ipady = 4)

        # @right_frame_top
        self._right_frame_top = Frame(self._right_frame, relief = GROOVE, borderwidth = 2)
        self._right_frame_top.pack(fill = BOTH, expand = TRUE, padx = 4, pady = 4, ipady = 4);

        # @right_frame_top_label
        self._right_frame_top_label = Label(self._right_frame_top, text = "Výsledek")
        self._right_frame_top_label.pack(fill = X)

        # @right_frame_top_result
        self._right_frame_top_result = Frame(self._right_frame_top, relief = SUNKEN, borderwidth = 2)
        self._right_frame_top_result.pack(fill = BOTH, expand = TRUE, padx = 8, pady = 4, ipady = 4)

        # @right_frame_top_result_label
        self._right_frame_top_result_label = Label(self._right_frame_top_result, text = "Zatím žádný výsledek", background = "#CCC")
        self._right_frame_top_result_label.pack(fill = BOTH, expand = TRUE, ipadx = 8, ipady = 8)

        # @right_frame_bottom
        self._right_frame_bottom = Frame(self._right_frame)
        self._right_frame_bottom.pack(fill = X)

        # @right_frame_bottom_center
        self._right_frame_bottom_center = Frame(self._right_frame_bottom)
        self._right_frame_bottom_center.pack(anchor = CENTER)

        # @right_frame_bottom_center_result_button
        self._result_button = Button(
            self._right_frame_bottom_center, 
            text = "Vyřešit", 
            command = self.solve
        )
        self._result_button.pack(side = LEFT, padx = 8, pady = 8, ipadx = 8, ipady = 8)

        # @right_frame_bottom_center_close_button
        self._close_button = Button(
            self._right_frame_bottom_center, 
            text = "Konec", 
            command = exit
        )
        self._close_button.pack(side = LEFT, padx = 8, pady = 8, ipadx = 8, ipady = 8)
        
        
    def reset(self):
        self._input_a.delete(0, END)
        self._input_a.insert(0, "0")
        self._input_a.focus_force()
        self._input_b.delete(0, END)
        self._input_b.insert(0, "0")
        self._input_c.delete(0, END)
        self._input_c.insert(0, "0")
        
    
    def solve(self):
        try:
            a = float(self._input_a.get())
            b = float(self._input_b.get())
            c = float(self._input_c.get())
            
            text = MyApplication.GENERAL_TRIANGLE
            if self.is_triangle(a, b, c):
                if self.is_right_triangle(a, b, c):
                    text = MyApplication.RIGHT_TRIANGLE
                if self.is_equilateral_triangle(a, b, c):
                    text = MyApplication.EQUILATERAL_TRIANGLE
                
                # TODO output of results
                self._right_frame_top_result_label.config(foreground = "#00F")
                obvod = "{:.2f}".format(self.compute_circumference(a, b, c))
                obsah = "{:.2f}".format(self.compute_area(a, b, c))
                output = text + "\n\nObvod: " + str(obvod) + "\nObsah: " + str(obsah)
            else:
                # TODO show error information, it is not a triangle
                self._right_frame_top_result_label.config(foreground = "#F00")
                output = "Není trojúhelník"
                pass
                
        except ValueError:
            # TODO show error message - wrong values
            self._right_frame_top_result_label.config(foreground = "#F00")
            output = "Chyba\n\nNejméně jedna strana nebyla zadána"
            pass
        self._right_frame_top_result_label.config(text = output)
        
        
    def compute_circumference(self, a, b, c):
        return a + b + c
    
    
    def compute_area(self, a, b, c):
        p = self.compute_circumference(a, b, c) / 2
        return sqrt(p * (p - a) * (p - b) * (p - c))
        
        
    def is_triangle(self, a, b, c):
        # TODO
        return (a + b > c) and (a + c > b) and (b + c > a)
        
        
    def is_right_triangle(self, a, b, c):
        # TODO
        return (a*a + b*b == c*c) or (a*a + c*c == b*b) or (b*b + c*c == a*a)
        
        
    def is_equilateral_triangle(self, a, b, c):
        # TODO
        return (a == b) and (b == c)
        

def main():
    root = Tk()
    app = MyApplication(root)
    root.mainloop()

main()
