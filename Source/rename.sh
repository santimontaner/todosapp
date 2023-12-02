#!/bin/bash

directorio="/home/santi/projects/mvc-test"

# Función para renombrar archivos y directorios recursivamente
renombrar_archivos_directorios() {
  local dir="$1"

  # Iterar sobre cada elemento en el directorio
  for elemento in "$dir"/*; do
    if [ -d "$elemento" ]; then
      # Si es un directorio, renombrar y llamar recursivamente a la función
      nuevo_nombre=$(echo "$elemento" | sed 's/MvcTest/TodosApp/g')
      if [ "$elemento" != "$nuevo_nombre" ]; then
        mv "$elemento" "$nuevo_nombre"
        echo "Renombrado directorio: $elemento -> $nuevo_nombre"
      fi
      renombrar_archivos_directorios "$nuevo_nombre"
    elif [ -f "$elemento" ]; then
      # Si es un archivo, renombrar
      nuevo_nombre=$(echo "$elemento" | sed 's/MvcTest/TodosApp/g')
      if [ "$elemento" != "$nuevo_nombre" ]; then
        mv "$elemento" "$nuevo_nombre"
        echo "Renombrado archivo: $elemento -> $nuevo_nombre"
      fi
    fi
  done
}

# Llamar a la función con el directorio principal
renombrar_archivos_directorios "$directorio"
