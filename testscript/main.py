db_path: str

str_to_file = f'Я должен был открывать файл EXEL по следующему пути {db_path} и делать модель,\n' \
                  f'но я сильно кастрирован из-за IronPyton и у меня лапки, поэтому только так'
model_path = 'C:\\Users\\navip\\OneDrive\\Документы\\GitHub\\Splav\\generated_model.bin'
with open(model_path, 'wb') as file:
    file.write(bytes(str_to_file))
