import os #Biblioteca que permite interagir com o sistema operacional
import shutil #Biblioteca que permite copiar e mover arquivos e pastas

#Definir o caminho
caminho = "." # . é o diretório atual

#Mapear as extensoes de arquivos para as suas respetivas pastas
diretorios = {
    "Imagens": [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff"],
    "Documentos": [".pdf", ".doc", ".docx", ".txt", ".xls", ".xlsx", ".ppt", ".pptx"],
    "Videos": [".mp4", ".avi", ".mov", ".mkv", ".flv"],
    "Musica": [".mp3", ".wav", ".aac", ".flac"],
    "Arquivos_Comprimidos": [".zip", ".rar", ".tar", ".gz"]
}

arquivos.listdir(.)

for arquivo in os.listdir(.):
    nome, extensao