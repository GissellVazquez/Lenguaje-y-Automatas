import tkinter as tk
from tkinter import messagebox
import networkx as nx
import matplotlib.pyplot as plt

# -----------------------------
# VARIABLES
# -----------------------------
estados = []
alfabeto = []
transiciones = {}
estados_finales = []
estado_inicial = None

# -----------------------------
# CONSTANTES
# -----------------------------
VACIO = "∅"
EPSILON = "ε"

# -----------------------------
# FUNCIONES AUXILIARES
# -----------------------------

def limpiar_lista(texto):
    return [x.strip() for x in texto.split(",") if x.strip()]

def es_vacio(exp):
    return exp == VACIO or exp == ""

def es_epsilon(exp):
    return exp == EPSILON

def parentizar(exp):
    if exp.startswith("(") and exp.endswith(")"):
        return exp
    return f"({exp})"

def unir(a,b):

    if es_vacio(a):
        return b
    if es_vacio(b):
        return a
    if a == b:
        return a

    return a + "+" + b

def concatenar(a,b):

    if es_vacio(a) or es_vacio(b):
        return VACIO

    if es_epsilon(a):
        return b

    if es_epsilon(b):
        return a

    return a + b

def estrella(a):

    if es_vacio(a) or es_epsilon(a):
        return EPSILON

    if a.endswith("*"):
        return a

    return parentizar(a) + "*"

# -----------------------------
# SIMPLIFICACION BASICA
# -----------------------------

def simplificar(exp):

    if not exp:
        return exp

    exp = exp.replace("(ε)","ε")

    while "((" in exp:
        exp = exp.replace("((","(")

    while "))" in exp:
        exp = exp.replace("))",")")

    return exp

# -----------------------------
# FUNCIONES BASICAS
# -----------------------------

def agregar_estados():
    global estados
    estados = limpiar_lista(entrada_estados.get())
    messagebox.showinfo("Info","Estados agregados")

def agregar_alfabeto():
    global alfabeto
    alfabeto = limpiar_lista(entrada_alfabeto.get())
    messagebox.showinfo("Info","Alfabeto agregado")

def agregar_finales():
    global estados_finales
    estados_finales = limpiar_lista(entrada_finales.get())
    messagebox.showinfo("Info","Estados finales agregados")

def agregar_inicial():

    global estado_inicial

    estado = entrada_inicial.get().strip()

    if not estado:
        messagebox.showwarning("Error","Ingrese un estado inicial")
        return

    if estados and estado not in estados:
        messagebox.showwarning("Error","El estado inicial no existe")
        return

    estado_inicial = estado

    messagebox.showinfo("Info",f"Estado inicial definido: {estado}")

# -----------------------------
# TRANSICIONES
# -----------------------------

def agregar_transicion():

    origen = entrada_origen.get().strip()
    simbolo = entrada_simbolo.get().strip()
    destino = entrada_destino.get().strip()

    if origen == "" or simbolo == "" or destino == "":
        messagebox.showwarning("Error","Complete todos los campos")
        return

    if estados and (origen not in estados or destino not in estados):
        messagebox.showwarning("Error","Origen o destino no existe")
        return

    if alfabeto and simbolo not in alfabeto:
        messagebox.showwarning("Error","Símbolo fuera del alfabeto")
        return

    if origen not in transiciones:
        transiciones[origen] = {}

    if simbolo not in transiciones[origen]:
        transiciones[origen][simbolo] = []

    transiciones[origen][simbolo].append(destino)

    lista_transiciones.insert(tk.END,f"{origen} --{simbolo}--> {destino}")

    entrada_origen.delete(0,tk.END)
    entrada_simbolo.delete(0,tk.END)
    entrada_destino.delete(0,tk.END)

def eliminar_transicion():

    seleccion = lista_transiciones.curselection()

    if not seleccion:
        messagebox.showwarning("Error","Seleccione una transición")
        return

    indice = seleccion[0]
    texto = lista_transiciones.get(indice)

    partes = texto.split()

    origen = partes[0]
    simbolo = partes[1].replace("--","").replace(">","")
    destino = partes[2]

    try:

        transiciones[origen][simbolo].remove(destino)

        if len(transiciones[origen][simbolo]) == 0:
            del transiciones[origen][simbolo]

        if len(transiciones[origen]) == 0:
            del transiciones[origen]

    except:
        pass

    lista_transiciones.delete(indice)

# -----------------------------
# DIBUJAR AUTOMATA
# -----------------------------

def dibujar_automata():

    if not estados:
        messagebox.showwarning("Error","Agregue estados")
        return

    G = nx.MultiDiGraph()

    for estado in estados:
        G.add_node(estado)

    for origen in transiciones:
        for simbolo in transiciones[origen]:
            for destino in transiciones[origen][simbolo]:
                G.add_edge(origen,destino,label=simbolo)

    pos = nx.spring_layout(G,seed=5)

    colores=[]

    for estado in G.nodes():

        if estado == estado_inicial:
            colores.append("orange")

        elif estado in estados_finales:
            colores.append("lightgreen")

        else:
            colores.append("lightblue")

    nx.draw(
        G,pos,
        with_labels=True,
        node_color=colores,
        node_size=2000,
        font_size=12,
        arrows=True
    )

    labels = {}

    for u,v,k,data in G.edges(keys=True,data=True):

        if (u,v) not in labels:
            labels[(u,v)] = data["label"]
        else:
            labels[(u,v)] += "+" + data["label"]

    nx.draw_networkx_edge_labels(G,pos,edge_labels=labels)

    plt.title("Autómata Finito")
    plt.show()

# -----------------------------
# GENERAR EXPRESION REGULAR
# -----------------------------

def generar_expresion():

    if not estados:
        messagebox.showwarning("Error","Agregue estados")
        return

    if estado_inicial is None:
        messagebox.showwarning("Error","Defina estado inicial")
        return

    if not estados_finales:
        messagebox.showwarning("Error","Defina estados finales")
        return

    n = len(estados)

    indice = {estado:i for i,estado in enumerate(estados)}

    A = [[VACIO for _ in range(n)] for _ in range(n)]
    B = [VACIO for _ in range(n)]

    for origen in estados:

        i = indice[origen]

        if origen in estados_finales:
            B[i] = EPSILON

        if origen in transiciones:

            for simbolo in transiciones[origen]:

                for destino in transiciones[origen][simbolo]:

                    j = indice[destino]

                    A[i][j] = unir(A[i][j],simbolo)

    for k in range(n-1,-1,-1):

        Akk_star = estrella(A[k][k])

        for j in range(n):

            if j != k and not es_vacio(A[k][j]):
                A[k][j] = concatenar(Akk_star,A[k][j])

        B[k] = concatenar(Akk_star,B[k])

        A[k][k] = VACIO

        for i in range(n):

            if i == k or es_vacio(A[i][k]):
                continue

            factor = A[i][k]

            for j in range(n):

                if j == k or es_vacio(A[k][j]):
                    continue

                A[i][j] = unir(A[i][j],concatenar(factor,A[k][j]))

            B[i] = unir(B[i],concatenar(factor,B[k]))

            A[i][k] = VACIO

    i0 = indice[estado_inicial]

    expresion_final = simplificar(B[i0])

    resultado.delete(0,tk.END)
    resultado.insert(0,expresion_final)

# -----------------------------
# INTERFAZ
# -----------------------------

ventana = tk.Tk()
ventana.title("Generador de Expresiones Regulares")
ventana.geometry("420x680")

# ESTADOS
tk.Label(ventana,text="Estados (q0,q1,q2)").pack()
entrada_estados = tk.Entry(ventana)
entrada_estados.pack()
tk.Button(ventana,text="Agregar Estados",command=agregar_estados).pack(pady=5)

# ESTADO INICIAL
tk.Label(ventana,text="Estado inicial").pack()
entrada_inicial = tk.Entry(ventana)
entrada_inicial.pack()
tk.Button(ventana,text="Agregar Estado Inicial",command=agregar_inicial).pack(pady=5)

# ALFABETO
tk.Label(ventana,text="Alfabeto (a,b)").pack()
entrada_alfabeto = tk.Entry(ventana)
entrada_alfabeto.pack()
tk.Button(ventana,text="Agregar Alfabeto",command=agregar_alfabeto).pack(pady=5)

# FINALES
tk.Label(ventana,text="Estados finales").pack()
entrada_finales = tk.Entry(ventana)
entrada_finales.pack()
tk.Button(ventana,text="Agregar Estados Finales",command=agregar_finales).pack(pady=5)

# TRANSICIONES
tk.Label(ventana,text="Origen").pack()
entrada_origen = tk.Entry(ventana)
entrada_origen.pack()

tk.Label(ventana,text="Símbolo").pack()
entrada_simbolo = tk.Entry(ventana)
entrada_simbolo.pack()

tk.Label(ventana,text="Destino").pack()
entrada_destino = tk.Entry(ventana)
entrada_destino.pack()

tk.Button(ventana,text="Agregar Transición",command=agregar_transicion).pack(pady=5)
tk.Button(ventana,text="Eliminar Transición",command=eliminar_transicion).pack(pady=5)

# LISTA
tk.Label(ventana,text="Transiciones").pack()
lista_transiciones = tk.Listbox(ventana,width=35,height=8)
lista_transiciones.pack()

# BOTONES
tk.Button(ventana,text="Dibujar Autómata",command=dibujar_automata).pack(pady=10)
tk.Button(ventana,text="Generar Expresión Regular",command=generar_expresion).pack()

# RESULTADO
tk.Label(ventana,text="Expresión Regular").pack()
resultado = tk.Entry(ventana,width=40)
resultado.pack(pady=5)

ventana.mainloop()