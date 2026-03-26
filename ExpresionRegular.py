import tkinter as tk
from tkinter import ttk
import networkx as nx
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg

contador_estado = 0


def nuevo_estado():
    global contador_estado
    estado = f"q{contador_estado}"
    contador_estado += 1
    return estado


class Automata:

    def __init__(self, inicio, fin, transiciones):
        self.inicio = inicio
        self.fin = fin
        self.transiciones = transiciones


def automata_simbolo(simbolo):

    inicio = nuevo_estado()
    fin = nuevo_estado()

    transiciones = [(inicio, fin, simbolo)]

    return Automata(inicio, fin, transiciones)


def union(a1, a2):

    inicio = nuevo_estado()
    fin = nuevo_estado()

    transiciones = []
    transiciones += a1.transiciones
    transiciones += a2.transiciones

    transiciones.append((inicio, a1.inicio, "e"))
    transiciones.append((inicio, a2.inicio, "e"))

    transiciones.append((a1.fin, fin, "e"))
    transiciones.append((a2.fin, fin, "e"))

    return Automata(inicio, fin, transiciones)


def concatenacion(a1, a2):

    transiciones = []
    transiciones += a1.transiciones
    transiciones += a2.transiciones

    transiciones.append((a1.fin, a2.inicio, "e"))

    return Automata(a1.inicio, a2.fin, transiciones)


def kleene(a1):

    inicio = nuevo_estado()
    fin = nuevo_estado()

    transiciones = []
    transiciones += a1.transiciones

    transiciones.append((inicio, a1.inicio, "e"))
    transiciones.append((inicio, fin, "e"))

    transiciones.append((a1.fin, a1.inicio, "e"))
    transiciones.append((a1.fin, fin, "e"))

    return Automata(inicio, fin, transiciones)


def construir(expresion):

    pila = []
    i = 0

    while i < len(expresion):

        c = expresion[i]

        if c.isalpha():

            auto = automata_simbolo(c)

            if pila and isinstance(pila[-1], Automata):
                anterior = pila.pop()
                auto = concatenacion(anterior, auto)

            pila.append(auto)

        elif c == "+":
            pila.append(c)

        elif c == "*":

            auto = pila.pop()
            pila.append(kleene(auto))

        elif c == "(":
            pila.append(c)

        elif c == ")":

            auto = pila.pop()
            pila.pop()
            pila.append(auto)

        if len(pila) >= 3 and pila[-2] == "+":
            a2 = pila.pop()
            pila.pop()
            a1 = pila.pop()
            pila.append(union(a1, a2))

        i += 1

    return pila[0]


def dibujar(automata):

    G = nx.DiGraph()

    for origen, destino, simbolo in automata.transiciones:
        G.add_edge(origen, destino, label=simbolo)

    pos = nx.spring_layout(G, k=1.5, seed=42)

    fig, ax = plt.subplots(figsize=(12,6))

    colores = []

    for estado in G.nodes():

        if estado == automata.inicio:
            colores.append("#8fd694")

        elif estado == automata.fin:
            colores.append("#f7a072")

        else:
            colores.append("#7aa6d8")

    nx.draw(
        G,
        pos,
        with_labels=True,
        node_color=colores,
        node_size=2000,
        font_size=11,
        font_weight="bold",
        edgecolors="black",
        arrows=True,
        arrowsize=20,
        width=2,
        connectionstyle="arc3,rad=0.1",
        ax=ax
    )

    labels = nx.get_edge_attributes(G, 'label')

    nx.draw_networkx_edge_labels(
        G,
        pos,
        edge_labels=labels,
        font_size=10,
        ax=ax
    )

    ax.set_title("Autómata Finito No Determinista", fontsize=10)
    ax.axis("off")

    return fig



def acepta(automata, estado, cadena, visitados):

    if (estado, cadena) in visitados:
        return False

    visitados.add((estado, cadena))

    if cadena == "":
        if estado == automata.fin:
            return True

    for origen, destino, simbolo in automata.transiciones:

        if origen == estado:

            if simbolo == "e":
                if acepta(automata, destino, cadena, visitados):
                    return True

            if cadena and simbolo == cadena[0]:
                if acepta(automata, destino, cadena[1:], visitados):
                    return True

    return False


def generar():

    global contador_estado
    contador_estado = 0

    expresion = entrada.get()

    automata = construir(expresion)

    fig = dibujar(automata)

    for widget in frame_grafo.winfo_children():
        widget.destroy()

    canvas = FigureCanvasTkAgg(fig, master=frame_grafo)
    canvas.draw()
    canvas.get_tk_widget().pack()


def verificar():

    expresion = entrada.get()
    cadena = entrada_cadena.get()

    global contador_estado
    contador_estado = 0

    automata = construir(expresion)

    resultado = acepta(automata, automata.inicio, cadena, set())

    if resultado:
        resultado_label.config(text="Cadena ACEPTADA", foreground="green")
    else:
        resultado_label.config(text="Cadena RECHAZADA", foreground="red")


#Interfaz

ventana = tk.Tk()
ventana.title("Generador de AFN desde Expresión Regular")
ventana.geometry("950x450")


frame_controles = ttk.Frame(ventana)
frame_controles.grid(row=0, column=0, padx=20, pady=20)


titulo = ttk.Label(frame_controles, text="Ingrese expresión regular")
titulo.pack(pady=10)


entrada = ttk.Entry(frame_controles, width=25)
entrada.pack()


boton = ttk.Button(
    frame_controles,
    text="Construir Autómata",
    command=generar
)

boton.pack(pady=10)


label_cadena = ttk.Label(frame_controles, text="Ingrese cadena")
label_cadena.pack(pady=5)


entrada_cadena = ttk.Entry(frame_controles, width=25)
entrada_cadena.pack()


boton_verificar = ttk.Button(
    frame_controles,
    text="Verificar Cadena",
    command=verificar
)

boton_verificar.pack(pady=10)


resultado_label = ttk.Label(frame_controles, text="")
resultado_label.pack(pady=10)


frame_grafo = ttk.Frame(ventana)
frame_grafo.grid(row=0, column=1, padx=20, pady=20)


ventana.mainloop()