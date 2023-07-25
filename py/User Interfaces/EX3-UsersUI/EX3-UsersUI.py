# -*- coding: utf-8 -*-

from tkinter import *
from tkinter import tix
import MultiListbox as table

NAME = 0
SURNAME = 1
BIRTH_NUMBER = 2
STREET = 3
NUMBER = 4
CITY = 5
ZIP = 6
NOTE = 7
data = [
        # Jméno, Příjmení, RČ,       ulice,         čp,   město,     PSČ,  poznámka
       ["Petr", "Bílý","045214/1512","17. Listopadu", 15, "Ostrava", 70800,"poznamka"],
       ["Jana", "Zelený","901121/7238","Vozovna", 54, "Poruba", 78511,""],
       ["Karel", "Modrý","800524/5417","Porubská", 7, "Praha", 11150,""],
       ["Martin", "Stříbrný","790407/3652","Sokolovská", 247, "Brno", 54788,"nic"]]


class App(object):
    def __init__(self, master):
        self._row = IntVar()
        self._row = None
        self._jmeno = StringVar()
        self._surname = StringVar()
        self._birth_number = StringVar()
        self._street = StringVar()
        self._number = StringVar()
        self._city = StringVar()
        self._zip = StringVar()
        self._note = StringVar()


        self._mlb = table.MultiListbox(master, (('Jméno', 20), ('Příjmení', 20), ('Rodné číslo', 12)))
        
        for i in range(len(data)):
            self._mlb.insert(END, (data[i][0], data[i][1],data[i][2]))
        self._mlb.pack(expand=YES,fill=BOTH, padx=10, pady=10)
        #self._mlb.subscribe( lambda row: self._edit( row ) )
        self._mlb.subscribe( self._edit)
        
        # menu - TODO
        self._menu = Menu(master)
        self._menu_file = Menu(self._menu, tearoff = 0)
        self._menu_options = Menu(self._menu, tearoff = 0)
        self._menu_help = Menu(self._menu, tearoff = 0)
        self._menu.add_cascade(label = "Soubor", menu = self._menu_file)
        self._menu.add_cascade(label = "Nastavení", menu = self._menu_options)
        self._menu.add_cascade(label = "Nápověda", menu = self._menu_help)
        master.config(menu = self._menu)
        
        # form - TODO
        self._formFrame = Frame(master)
        self._formFrame.pack(expand=YES, padx=10, pady=10)
        self._formFrame.grid_columnconfigure(0, weight = 1)
        self._formFrame.grid_columnconfigure(1, weight = 1)
        
        self._formJmenoLabel = Label(self._formFrame, text = "Jméno")
        self._formJmenoLabel.grid(row = 0, column = 0, sticky = W+E+N+S, padx = (20,10), pady = (20,5))
        self._formJmenoEntry = Entry(self._formFrame, width = 30)
        self._formJmenoEntry.grid(row = 0, column = 1, padx = (10,20), pady = (20,5), sticky = W)
        self._formSurnameLabel = Label(self._formFrame, text = "Příjmení")
        self._formSurnameLabel.grid(row = 1, column = 0, sticky = W+E+N+S, padx = (20,10), pady = 5)
        self._formSurnameEntry = Entry(self._formFrame, width = 30)
        self._formSurnameEntry.grid(row = 1, column = 1, padx = (10,20), pady = 5, sticky = W)
        self._formBirthNumberLabel = Label(self._formFrame, text = "Rodné číslo")
        self._formBirthNumberLabel.grid(row = 2, column = 0, sticky = W+E+N+S, padx = (20,10), pady = (5,20))
        self._formBirthNumberEntry = Entry(self._formFrame, width = 15)
        self._formBirthNumberEntry.grid(row = 2, column = 1, padx = (10,20), pady = (5,20), sticky = W)
        
        # tabs - TODO
        self._nb = tix.NoteBook(master)
        self._nb.add("address", label="Adresa")
        self._nb.add("note", label="Poznámka")

        self._address = self._nb.subwidget_list["address"]
        self._note = self._nb.subwidget_list["note"]
        self._nb.pack(expand = 1, fill = BOTH)

        self._addressFrame = LabelFrame(self._address, text="Adresa")
        self._addressFrame.pack(expand=YES,fill=BOTH, padx=10, pady=10)
        self._addressFrame.grid_columnconfigure(0, weight = 1)
        self._addressFrame.grid_columnconfigure(1, weight = 1)
        self._addressFrame.grid_columnconfigure(2, weight = 1)
        self._addressFrame.grid_columnconfigure(3, weight = 1)
        self._noteFrame = LabelFrame(self._note, text="Poznámka")
        self._noteFrame.pack(expand=YES,fill=BOTH, padx=10, pady=10)

        self._addressStreetLabel = Label(self._addressFrame, text = "Ulice")
        self._addressStreetLabel.grid(row = 0, column = 0, sticky = W+E+N+S, padx = (20,10), pady = (20,5))
        self._addressStreetEntry = Entry(self._addressFrame, width = 20)
        self._addressStreetEntry.grid(row = 0, column = 1, padx = (10,20), pady = (20,5), sticky = W)
        self._addressCityLabel = Label(self._addressFrame, text = "Město")
        self._addressCityLabel.grid(row = 1, column = 0, sticky = W+E+N+S, padx = (20,10), pady = 5)
        self._addressCityEntry = Entry(self._addressFrame, width = 30)
        self._addressCityEntry.grid(row = 1, column = 1, padx = (10,20), pady = 5, sticky = W)
        self._addressNumberLabel = Label(self._addressFrame, text = "č.p.")
        self._addressNumberLabel.grid(row = 1, column = 2, sticky = W+E+N+S, padx = 10, pady = 5)
        self._addressNumberEntry = Entry(self._addressFrame, width = 10)
        self._addressNumberEntry.grid(row = 1, column = 3, padx = (10,20), pady = 5, sticky = W)
        self._addressZipLabel = Label(self._addressFrame, text = "PSČ")
        self._addressZipLabel.grid(row = 2, column = 0, sticky = W+E+N+S, padx = (20,10), pady = (5,20))
        self._addressZipEntry = Entry(self._addressFrame, width = 10)
        self._addressZipEntry.grid(row = 2, column = 1, padx = (10,20), pady = (5,20), sticky = W)
        self._noteNoteLabel = Label(self._noteFrame, text = "Poznámka")
        self._noteNoteLabel.grid(row = 1, column = 0, sticky = W+E+N+S, padx = (20,10), pady = 20)
        self._noteNoteEntry = Entry(self._noteFrame, width = 30)
        self._noteNoteEntry.grid(row = 1, column = 1, padx = (10,20), pady = 20, sticky = W)
        
        # buttons - TODO
        self._buttonsFrame = Frame(master)
        self._buttonsFrame.pack(expand=YES, padx=10, pady=10)
        self._buttonsFrame.grid_columnconfigure(0, weight = 1)
        self._buttonsFrame.grid_columnconfigure(1, weight = 1)
        self._buttonsFrame.grid_columnconfigure(2, weight = 1)
        self._buttonsCancelButton = Button(self._buttonsFrame, width = 20, text="Konec")
        self._buttonsCancelButton.grid(row = 0, column = 0, pady = 5)
        self._buttonsAddButton = Button(self._buttonsFrame, width = 20, text="Nový záznam")
        self._buttonsAddButton.grid(row = 0, column = 1, pady = 5)
        self._buttonsSaveButton = Button(self._buttonsFrame, width = 20, text="Uložit záznam")
        self._buttonsSaveButton.grid(row = 0, column = 2, pady = 5)
      
    def _edit(self, row):
        self._row=row
        
        # assign actual values to variables - TODO
        self._jmeno = data[row][NAME]
        self._surname = data[row][SURNAME]
        self._birth_number = data[row][BIRTH_NUMBER]
        self._street = data[row][STREET]
        self._number = data[row][NUMBER]
        self._city = data[row][CITY]
        self._zip = data[row][ZIP]
        self._note = data[row][NOTE]
        
        # deleting - TODO
        self._formJmenoEntry.delete(0, END)
        self._formSurnameEntry.delete(0, END)
        self._formBirthNumberEntry.delete(0, END)
        self._addressStreetEntry.delete(0, END)
        self._addressCityEntry.delete(0, END)
        self._addressNumberEntry.delete(0, END)
        self._addressZipEntry.delete(0, END)
        self._noteNoteEntry.delete(0, END)
        
        # inserting - TODO
        self._formJmenoEntry.insert(END, self._jmeno)
        self._formSurnameEntry.insert(END, self._surname)
        self._formBirthNumberEntry.insert(END, self._birth_number)
        self._addressStreetEntry.insert(END, self._street)
        self._addressCityEntry.insert(END, self._city)
        self._addressNumberEntry.insert(END, self._number)
        self._addressZipEntry.insert(END, self._zip)
        self._noteNoteEntry.insert(END, self._note)
        
        # new window - TODO
        
        print (row)
             
def main():
    root = tix.Tk()
    root.wm_title("Formulář")
    app = App(root)
    root.mainloop()
    
main()

