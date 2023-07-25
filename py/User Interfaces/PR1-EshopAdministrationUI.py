# -*- coding: utf-8 -*-

import math
from tkinter import *
import tkinter.scrolledtext as tkst

class App(object):
    TITLE = "Administrace e-shopu"
    PRODUCTS = {}
    CATEGORIES = {"Ostatní": {"category": "", "code": "Ostatní", "description": "Ostatní produkty nepatřící do žádné jiné kategorie.", "hide": 0}}
    ORDERS = []
    
    def __init__(self, root):
        self._root = root
        self._root.title(self.TITLE)

        # Menu
        self._menu = Menu(self._root)
        self._menu_file = Menu(self._menu, tearoff = 0)
        self._menu_view = Menu(self._menu, tearoff = 0)
        self._menu_options = Menu(self._menu, tearoff = 0)
        self._menu_help = Menu(self._menu, tearoff = 0)
        self._menu.add_cascade(label = "Soubor", menu = self._menu_file)
        self._menu_file.add_command(label = "Konec", command = self.exit)
        self._menu.add_cascade(label = "Zobrazení", menu = self._menu_view)
        self._menu.add_cascade(label = "Nastavení", menu = self._menu_options)
        self._menu.add_cascade(label = "Nápověda", menu = self._menu_help)
        self._root.config(menu = self._menu)
        
        # Tabs
        self._tabs = Frame(self._root)
        self._tabs.pack(fill = X, pady = (6,0))

        # Tabs / Buttons
        self._tabs_products = Button(self._tabs, text = "Produkty", borderwidth = 0, padx = 12, pady = 6, relief = FLAT)
        self._tabs_products.pack(side = LEFT)
        self._tabs_categories = Button(self._tabs, text = "Kategorie", borderwidth = 0, padx = 12, pady = 6, relief = FLAT)
        self._tabs_categories.pack(side = LEFT)
        self._tabs_orders = Button(self._tabs, text = "Objednávky", borderwidth = 0, padx = 12, pady = 6, relief = FLAT)
        self._tabs_orders.pack(side = LEFT)

        # Content
        self._content = Frame(self._root, bg = "#FFF")
        self._content.pack(expand = TRUE, fill = BOTH)

        # Content / Products
        self._content_products = Frame(self._content, bg = "#FFF")
        self._content_products.pack()

        # Content / Products / Left
        self._content_products_left = Frame(self._content_products, bg = "#FFF")
        self._content_products_left.grid(row = 0, column = 0, padx = (3,0), pady = 3, sticky = N)
        
        self._content_products_left_add = LabelFrame(self._content_products_left, text = "Přidat produkt", bg = "#FFF", padx = 9, pady = 9)
        self._content_products_left_add.grid(row = 0, padx = 3, pady = 3)
        self._content_products_left_add.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_products_left_add.grid_columnconfigure(7, weight = 1, minsize = 50)
        
        self._content_products_left_add_category_label = Label(self._content_products_left_add, text = "Kategorie", bg = "#FFF", anchor = W)
        self._content_products_left_add_category_label.grid(row = 0, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        products_add_category = StringVar()
        products_add_category.set("Ostatní")
        self._content_products_left_add_category_option = OptionMenu(self._content_products_left_add, products_add_category, *self.CATEGORIES.keys())
        self._content_products_left_add_category_option.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_products_left_add_category_option.grid(row = 0, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_name_label = Label(self._content_products_left_add, text = "Název produktu", bg = "#FFF", anchor = W)
        self._content_products_left_add_name_label.grid(row = 1, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_name_entry = Entry(self._content_products_left_add)
        self._content_products_left_add_name_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_name_entry.grid(row = 1, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_code_label = Label(self._content_products_left_add, text = "Kód produktu", bg = "#FFF", anchor = W)
        self._content_products_left_add_code_label.grid(row = 2, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_code_entry = Entry(self._content_products_left_add)
        self._content_products_left_add_code_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_code_entry.grid(row = 2, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_price_label = Label(self._content_products_left_add, text = "Cena (s DPH)", bg = "#FFF", anchor = W)
        self._content_products_left_add_price_label.grid(row = 3, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_price_entry = Entry(self._content_products_left_add)
        self._content_products_left_add_price_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_price_entry.grid(row = 3, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_weight_label = Label(self._content_products_left_add, text = "Váha (kg)", bg = "#FFF", anchor = W)
        self._content_products_left_add_weight_label.grid(row = 4, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_weight_entry = Entry(self._content_products_left_add)
        self._content_products_left_add_weight_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_weight_entry.grid(row = 4, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_date_label = Label(self._content_products_left_add, text = "Datum výroby", bg = "#FFF", anchor = W)
        self._content_products_left_add_date_label.grid(row = 5, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_date_entry = Entry(self._content_products_left_add)
        self._content_products_left_add_date_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_date_entry.grid(row = 5, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_color_label = Label(self._content_products_left_add, text = "Barva", bg = "#FFF", anchor = W)
        self._content_products_left_add_color_label.grid(row = 6, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_color_entry = Entry(self._content_products_left_add)
        self._content_products_left_add_color_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_color_entry.grid(row = 6, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_description_label = Label(self._content_products_left_add, text = "Podrobnosti", bg = "#FFF", anchor = W)
        self._content_products_left_add_description_label.grid(row = 7, column = 0, columnspan = 8, sticky = W+E+N+S, padx = 3, pady = 3)
        
        self._content_products_left_add_description_text = tkst.ScrolledText(self._content_products_left_add, width = 50, height = 10)
        self._content_products_left_add_description_text.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_description_text.grid(row = 8, column = 0, columnspan = 8, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_left_add_hide_label = Label(self._content_products_left_add, text = "Skrýt produkt", bg = "#FFF", anchor = W)
        self._content_products_left_add_hide_label.grid(row = 9, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        products_add_hide = StringVar(self._root, "1")
        self._content_products_left_add_hide_true = Radiobutton(self._content_products_left_add, text = "ANO", variable = products_add_hide, value = "1")
        self._content_products_left_add_hide_true.config(bg = "#FFF", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_products_left_add_hide_true.grid(row = 9, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_hide_false = Radiobutton(self._content_products_left_add, text = "NE", variable = products_add_hide, value = "0")
        self._content_products_left_add_hide_false.config(bg = "#FFF", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_products_left_add_hide_false.grid(row = 9, column = 6, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        
        self._content_products_left_add_add_button = Button(self._content_products_left_add, text = "Přidat")
        self._content_products_left_add_add_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_add_button.grid(row = 10, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_left_add_reset_button = Button(self._content_products_left_add, text = "Reset")
        self._content_products_left_add_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_left_add_reset_button.grid(row = 10, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        
        # Content / Products / Right
        self._content_products_right = Frame(self._content_products, bg = "#FFF")
        self._content_products_right.grid(row = 0, column = 1, padx = (0,3), pady = 3, sticky = N)
        
        self._content_products_right_search = LabelFrame(self._content_products_right, text = "Vyhledat produkt", bg = "#FFF", padx = 9, pady = 9)
        self._content_products_right_search.grid(row = 0, padx = 3, pady = 3)
        self._content_products_right_search.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_products_right_search.grid_columnconfigure(7, weight = 1, minsize = 50)

        self._content_products_right_search_search_entry = Entry(self._content_products_right_search)
        self._content_products_right_search_search_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_right_search_search_entry.grid(row = 0, column = 0, columnspan = 8, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_right_search_search_button = Button(self._content_products_right_search, text = "Vyhledat")
        self._content_products_right_search_search_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_right_search_search_button.grid(row = 1, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_right_search_reset_button = Button(self._content_products_right_search, text = "Reset")
        self._content_products_right_search_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_right_search_reset_button.grid(row = 1, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_right_remove = LabelFrame(self._content_products_right, text = "Odebrat produkt", bg = "#FFF", padx = 9, pady = 9)
        self._content_products_right_remove.grid(row = 1, padx = 3, pady = 3)
        self._content_products_right_remove.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_products_right_remove.grid_columnconfigure(7, weight = 1, minsize = 50)

        self._content_products_right_remove_code_label = Label(self._content_products_right_remove, text = "Kód produktu", bg = "#FFF", anchor = W)
        self._content_products_right_remove_code_label.grid(row = 0, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_right_remove_code_entry = Entry(self._content_products_right_remove)
        self._content_products_right_remove_code_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_right_remove_code_entry.grid(row = 0, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_products_right_remove_remove_button = Button(self._content_products_right_remove, text = "Odebrat")
        self._content_products_right_remove_remove_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_right_remove_remove_button.grid(row = 1, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_products_right_remove_reset_button = Button(self._content_products_right_remove, text = "Reset")
        self._content_products_right_remove_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_products_right_remove_reset_button.grid(row = 1, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)

        # Content / Categories
        self._content_categories = Frame(self._content, bg = "#FFF")
        self._content_categories.pack()

        # Content / Categories / Left
        self._content_categories_left = Frame(self._content_categories, bg = "#FFF")
        self._content_categories_left.grid(row = 0, column = 0, padx = (3,0), pady = 3, sticky = N)
        
        self._content_categories_left_add = LabelFrame(self._content_categories_left, text = "Přidat kategorii", bg = "#FFF", padx = 9, pady = 9)
        self._content_categories_left_add.grid(row = 0, padx = 3, pady = 3)
        self._content_categories_left_add.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_categories_left_add.grid_columnconfigure(7, weight = 1, minsize = 50)
        
        self._content_categories_left_add_category_label = Label(self._content_categories_left_add, text = "Vložit do kategorie", bg = "#FFF", anchor = W)
        self._content_categories_left_add_category_label.grid(row = 0, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        categories_add_category = StringVar()
        categories_add_category.set("Ostatní")
        self._content_categories_left_add_category_option = OptionMenu(self._content_categories_left_add, categories_add_category, *self.CATEGORIES)
        self._content_categories_left_add_category_option.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_categories_left_add_category_option.grid(row = 0, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_categories_left_add_name_label = Label(self._content_categories_left_add, text = "Název kategorie", bg = "#FFF", anchor = W)
        self._content_categories_left_add_name_label.grid(row = 1, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_categories_left_add_name_entry = Entry(self._content_categories_left_add)
        self._content_categories_left_add_name_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_categories_left_add_name_entry.grid(row = 1, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_categories_left_add_description_label = Label(self._content_categories_left_add, text = "Podrobnosti", bg = "#FFF", anchor = W)
        self._content_categories_left_add_description_label.grid(row = 2, column = 0, columnspan = 8, sticky = W+E+N+S, padx = 3, pady = 3)
        
        self._content_categories_left_add_description_text = tkst.ScrolledText(self._content_categories_left_add, width = 50, height = 10)
        self._content_categories_left_add_description_text.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_categories_left_add_description_text.grid(row = 3, column = 0, columnspan = 8, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_categories_left_add_hide_label = Label(self._content_categories_left_add, text = "Skrýt kategorii", bg = "#FFF", anchor = W)
        self._content_categories_left_add_hide_label.grid(row = 4, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        categories_add_hide = StringVar(self._root, "1")
        self._content_categories_left_add_hide_true = Radiobutton(self._content_categories_left_add, text = "ANO", variable = categories_add_hide, value = "1")
        self._content_categories_left_add_hide_true.config(bg = "#FFF", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_categories_left_add_hide_true.grid(row = 4, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_categories_left_add_hide_false = Radiobutton(self._content_categories_left_add, text = "NE", variable = categories_add_hide, value = "0")
        self._content_categories_left_add_hide_false.config(bg = "#FFF", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_categories_left_add_hide_false.grid(row = 4, column = 6, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        
        self._content_categories_left_add_add_button = Button(self._content_categories_left_add, text = "Přidat")
        self._content_categories_left_add_add_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_categories_left_add_add_button.grid(row = 5, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_categories_left_add_reset_button = Button(self._content_categories_left_add, text = "Reset")
        self._content_categories_left_add_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_categories_left_add_reset_button.grid(row = 5, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        
        # Content / Categories / Right
        self._content_categories_right = Frame(self._content_categories, bg = "#FFF")
        self._content_categories_right.grid(row = 0, column = 1, padx = (0,3), pady = 3, sticky = N)
        
        self._content_categories_right_remove = LabelFrame(self._content_categories_right, text = "Odebrat kategorii", bg = "#FFF", padx = 9, pady = 9)
        self._content_categories_right_remove.grid(row = 0, padx = 3, pady = 3)
        self._content_categories_right_remove.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_categories_right_remove.grid_columnconfigure(7, weight = 1, minsize = 50)

        self._content_categories_right_remove_code_label = Label(self._content_categories_right_remove, text = "Kategorie", bg = "#FFF", anchor = W)
        self._content_categories_right_remove_code_label.grid(row = 0, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        categories_remove_category = StringVar()
        categories_remove_category.set("Ostatní")
        self._content_categories_right_remove_code_option = OptionMenu(self._content_categories_right_remove, categories_remove_category, *self.CATEGORIES)
        self._content_categories_right_remove_code_option.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0, anchor = W)
        self._content_categories_right_remove_code_option.grid(row = 0, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_categories_right_remove_remove_button = Button(self._content_categories_right_remove, text = "Odebrat")
        self._content_categories_right_remove_remove_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_categories_right_remove_remove_button.grid(row = 1, column = 3, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)

        # Content / Orders
        self._content_orders = Frame(self._content, bg = "#FFF")
        self._content_orders.pack()

        # Content / Orders / Left
        self._content_orders_left = Frame(self._content_orders, bg = "#FFF")
        self._content_orders_left.grid(row = 0, column = 0, padx = (3,0), pady = 3, sticky = N)
        
        self._content_orders_left_add = LabelFrame(self._content_orders_left, text = "Přidat objednávku", bg = "#FFF", padx = 9, pady = 9)
        self._content_orders_left_add.grid(row = 0, padx = 3, pady = 3)
        self._content_orders_left_add.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_orders_left_add.grid_columnconfigure(7, weight = 1, minsize = 50)
        
        self._content_orders_left_add_code_label = Label(self._content_orders_left_add, text = "Kód produktu", bg = "#FFF", anchor = W)
        self._content_orders_left_add_code_label.grid(row = 0, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_code_entry = Entry(self._content_orders_left_add)
        self._content_orders_left_add_code_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_code_entry.grid(row = 0, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_left_add_quantity_label = Label(self._content_orders_left_add, text = "Počet kusů", bg = "#FFF", anchor = W)
        self._content_orders_left_add_quantity_label.grid(row = 1, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_quantity_entry = Entry(self._content_orders_left_add)
        self._content_orders_left_add_quantity_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_quantity_entry.grid(row = 1, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_left_add_firstName_label = Label(self._content_orders_left_add, text = "Jméno", bg = "#FFF", anchor = W)
        self._content_orders_left_add_firstName_label.grid(row = 2, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_firstName_entry = Entry(self._content_orders_left_add)
        self._content_orders_left_add_firstName_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_firstName_entry.grid(row = 2, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_left_add_lastName_label = Label(self._content_orders_left_add, text = "Příjmení", bg = "#FFF", anchor = W)
        self._content_orders_left_add_lastName_label.grid(row = 3, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_lastName_entry = Entry(self._content_orders_left_add)
        self._content_orders_left_add_lastName_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_lastName_entry.grid(row = 3, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_left_add_address_label = Label(self._content_orders_left_add, text = "Adresa", bg = "#FFF", anchor = W)
        self._content_orders_left_add_address_label.grid(row = 4, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_address_entry = Entry(self._content_orders_left_add)
        self._content_orders_left_add_address_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_address_entry.grid(row = 4, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_left_add_phone_label = Label(self._content_orders_left_add, text = "Telefon", bg = "#FFF", anchor = W)
        self._content_orders_left_add_phone_label.grid(row = 5, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_phone_entry = Entry(self._content_orders_left_add)
        self._content_orders_left_add_phone_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_phone_entry.grid(row = 5, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_left_add_add_button = Button(self._content_orders_left_add, text = "Přidat")
        self._content_orders_left_add_add_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_add_button.grid(row = 6, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_left_add_reset_button = Button(self._content_orders_left_add, text = "Reset")
        self._content_orders_left_add_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_left_add_reset_button.grid(row = 6, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        
        # Content / Orders / Right
        self._content_orders_right = Frame(self._content_orders, bg = "#FFF")
        self._content_orders_right.grid(row = 0, column = 1, padx = (0,3), pady = 3, sticky = N)
        
        self._content_orders_right_search = LabelFrame(self._content_orders_right, text = "Vyhledat objednávku", bg = "#FFF", padx = 9, pady = 9)
        self._content_orders_right_search.grid(row = 0, padx = 3, pady = 3)
        self._content_orders_right_search.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_orders_right_search.grid_columnconfigure(7, weight = 1, minsize = 50)

        self._content_orders_right_search_search_entry = Entry(self._content_orders_right_search)
        self._content_orders_right_search_search_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_right_search_search_entry.grid(row = 0, column = 0, columnspan = 8, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_right_search_search_button = Button(self._content_orders_right_search, text = "Vyhledat")
        self._content_orders_right_search_search_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_right_search_search_button.grid(row = 1, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_right_search_reset_button = Button(self._content_orders_right_search, text = "Reset")
        self._content_orders_right_search_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_right_search_reset_button.grid(row = 1, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_right_remove = LabelFrame(self._content_orders_right, text = "Odebrat objednávku", bg = "#FFF", padx = 9, pady = 9)
        self._content_orders_right_remove.grid(row = 1, padx = 3, pady = 3)
        self._content_orders_right_remove.grid_columnconfigure(0, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(1, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(2, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(3, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(4, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(5, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(6, weight = 1, minsize = 50)
        self._content_orders_right_remove.grid_columnconfigure(7, weight = 1, minsize = 50)

        self._content_orders_right_remove_id_label = Label(self._content_orders_right_remove, text = "ID objednávky", bg = "#FFF", anchor = W)
        self._content_orders_right_remove_id_label.grid(row = 0, column = 0, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_right_remove_id_entry = Entry(self._content_orders_right_remove)
        self._content_orders_right_remove_id_entry.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_right_remove_id_entry.grid(row = 0, column = 4, columnspan = 4, sticky = W+E+N+S, padx = 3, pady = 3)

        self._content_orders_right_remove_remove_button = Button(self._content_orders_right_remove, text = "Odebrat")
        self._content_orders_right_remove_remove_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_right_remove_remove_button.grid(row = 1, column = 2, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        self._content_orders_right_remove_reset_button = Button(self._content_orders_right_remove, text = "Reset")
        self._content_orders_right_remove_reset_button.config(bg = "#EEE", borderwidth = 1, highlightthickness = 0)
        self._content_orders_right_remove_reset_button.grid(row = 1, column = 4, columnspan = 2, sticky = W+E+N+S, padx = 3, pady = 3)
        
        # Contents
        self.CONTENTS = [
            {"title":"Produkty", "tab":self._tabs_products, "content":self._content_products},
            {"title":"Kategorie", "tab":self._tabs_categories, "content":self._content_categories},
            {"title":"Objednávky", "tab":self._tabs_orders, "content":self._content_orders}
        ]

        # Set default content
        self.show_content(0)

        # Tabs / Buttons / Events
        self._tabs_products.configure(command = lambda: self.show_content(0))
        self._tabs_categories.configure(command = lambda: self.show_content(1))
        self._tabs_orders.configure(command = lambda: self.show_content(2))

        # Events / Products
        self._content_products_left_add_add_button.configure(command = lambda: self.add_product(self._content_products_left_add_code_entry.get(),{
            "category": products_add_category.get(),
            "name": self._content_products_left_add_name_entry.get(),
            "code": self._content_products_left_add_code_entry.get(),
            "price": self._content_products_left_add_price_entry.get(),
            "weight": self._content_products_left_add_weight_entry.get(),
            "date": self._content_products_left_add_date_entry.get(),
            "color": self._content_products_left_add_color_entry.get(),
            "description": self._content_products_left_add_description_text.get(1.0, END),
            "hide": products_add_hide.get()
        }))
        self._content_products_left_add_reset_button.configure(command = lambda: self.reset({
            "Category": [
                products_add_category
            ],
            "Entry": [
                self._content_products_left_add_name_entry,
                self._content_products_left_add_code_entry,
                self._content_products_left_add_price_entry,
                self._content_products_left_add_weight_entry,
                self._content_products_left_add_date_entry,
                self._content_products_left_add_color_entry
            ],
            "Radio": [
                products_add_hide
            ],
            "Text": [
                self._content_products_left_add_description_text
            ]
        }))
        self._content_products_right_search_reset_button.configure(command = lambda: self.reset({
            "Entry": [
                self._content_products_right_search_search_entry
            ]
        }))
        self._content_products_right_remove_reset_button.configure(command = lambda: self.reset({
            "Entry": [
                self._content_products_right_remove_code_entry
            ]
        }))

        # Events / Categories
        self._content_categories_left_add_add_button.configure(command = lambda: self.add_category(self._content_categories_left_add_name_entry.get(),{
            "category": categories_add_category.get(),
            "name": self._content_categories_left_add_name_entry.get(),
            "description": self._content_categories_left_add_description_text.get(1.0, END),
            "hide": categories_add_hide.get()
        }))
        self._content_categories_left_add_reset_button.configure(command = lambda: self.reset({
            "Category": [
                categories_add_category
            ],
            "Entry": [
                self._content_categories_left_add_name_entry
            ],
            "Radio": [
                categories_add_hide
            ],
            "Text": [
                self._content_categories_left_add_description_text
            ]
        }))

        # Events / Orders
        self._content_orders_left_add_add_button.configure(command = lambda: self.add_order(self.ORDERS,{
            "code": self._content_orders_left_add_code_entry.get(),
            "quantity": self._content_orders_left_add_quantity_entry.get(),
            "firstName": self._content_orders_left_add_firstName_entry.get(),
            "lastName": self._content_orders_left_add_lastName_entry.get(),
            "address": self._content_orders_left_add_address_entry.get(),
            "phone": self._content_orders_left_add_phone_entry.get()
        }))
        self._content_orders_left_add_reset_button.configure(command = lambda: self.reset({
            "Entry": [
                self._content_orders_left_add_code_entry,
                self._content_orders_left_add_quantity_entry,
                self._content_orders_left_add_firstName_entry,
                self._content_orders_left_add_lastName_entry,
                self._content_orders_left_add_address_entry,
                self._content_orders_left_add_phone_entry
            ]
        }))
        self._content_orders_right_search_reset_button.configure(command = lambda: self.reset({
            "Entry": [
                self._content_orders_right_search_search_entry
            ]
        }))
        self._content_orders_right_remove_reset_button.configure(command = lambda: self.reset({
            "Entry": [
                self._content_orders_right_remove_id_entry
            ]
        }))

    def show_content(self, content_id):
        content = self.CONTENTS[content_id]

        # Title
        self._root.title(content.get("title") + " - " + self.TITLE)

        # Hide content
        self.hide_content()

        # Show content
        content.get("tab").config(bg = "#FFF")
        content.get("content").pack()

    def hide_content(self):
        for content in self.CONTENTS:
            content.get("tab").config(bg = "#EEE")
            content.get("content").pack_forget()
    
    def add_product(self, key, value):
        self.PRODUCTS[key] = value

    def add_category(self, key, value):
        self.CATEGORIES[key] = value
        if source == self.CATEGORIES:
            self._content_products_left_add_category_option["menu"].add_command(label = key)
            self._content_categories_left_add_category_option["menu"].add_command(label = key)
            self._content_categories_right_remove_code_option["menu"].add_command(label = key)

    def add_order(self, value):
        source.append(value)

    def reset(self, elements):
        if "Category" in elements:
            for category in elements["Category"]:
                category.set("Ostatní")
        if "Entry" in elements:
            for entry in elements["Entry"]:
                entry.delete(0, END)
        if "Radio" in elements:
            for radios in elements["Radio"]:
                radios.set("1")
        if "Text" in elements:
            for text in elements["Text"]:
                text.delete(1.0, END)
    
    def exit(self):
        self._root.destroy()

def main():
    root = Tk()
    app = App(root)
    root.mainloop()

main()
