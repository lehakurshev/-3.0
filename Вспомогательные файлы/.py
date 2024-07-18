import pandas as pd
import datetime
import random

import pandas as pd

# Загрузка данных из файла CSV
data = pd.read_csv("Football player_stats .csv")

# Удаление первого и последнего символа у всех значений в столбце "Gender)"
data["Gender)"] = data["Gender)"].str[:-1]
data["a"] = ""

# Удаление столбца "DateOfBirth"
data.drop("DateOfBirth", axis=1, inplace=True)
# Сохранение измененных данных в новый файл CSV
data.to_csv("Football player_stats_updated .csv", index=False)
