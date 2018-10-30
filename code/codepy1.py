import math
from typing import List

def fold_to(distance):
    initial_thickness = 0.0001
    if distance < initial_thickness:
        return None
    elif distance == initial_thickness:
        return 0
    else:
        return math.ceil(math.log2(distance / initial_thickness))

def number(bus_stops):
    acc = 0
    for array in bus_stops:
        acc += array[0]
        acc -= array[1]
    return acc

def solve(arr: List[str]) -> List[int]:
    ret = []
    for word in arr:
        word = word.lower()
        acc = 0
        for index in range(0, len(word)):
            char = word[index]
            if index == (ord(char) - ord('a')):
                acc += 1
        ret.append(acc)
    return ret

def encode(message, key):
    keys = [ int(num) for num in str(key) ]
    ret = []
    for index in range(0, len(message)):
        keyIndex = index % len(keys)
        value = ord(message[index]) - ord('a') + 1 + keys[keyIndex]
        ret.append(value)
    return ret

def string_clean(s: str) -> str:
    ret = ""
    for char in s:
        if not str.isdigit(char):
            ret += char
    return ret

def human_years_cat_years_dog_years(human_years):
    cat_age_rates = [15, 9, 4]
    dog_age_rates = [15, 9, 5]
    cat_age = 0
    dog_age = 0
    age_iter = human_years
    while age_iter >= 1:
        if age_iter == 1:
            cat_age += cat_age_rates[0]
            dog_age += dog_age_rates[0]
        elif age_iter == 2:
            cat_age += cat_age_rates[1]
            dog_age += dog_age_rates[1]
        else:
            cat_age += cat_age_rates[2]
            dog_age += dog_age_rates[2]
        age_iter -= 1
    return [human_years, cat_age, dog_age]

def bin_to_decimal(inp: str):
    length = len(inp)
    acc = 0
    for i in range(0, length):
        if inp[i] == "1":
            acc += 2 ** (length - i - 1)
    return acc

def remove(s: str) -> str:
    ret = ""
    flag = False
    for c in s[::-1]:
        if c == '!' and not flag:
            ret += c
        elif c != '!' and not flag:
            ret += c
            flag = True
        elif c == '!' and flag:
            continue
        elif c != '!' and flag:
            ret += c
    return ret[::-1]

def initialize_names(name: str) -> str:
    names = name.split(' ')
    if len(names) == 1:
        return names[0]
    elif len(names) == 2:
        return names[0] + ' ' + names[1]
    else:
        for index in range(1, len(names) - 1):
            temp = names[index][0] + '.'
            names[index] = temp
        return ' '.join(names)

from typing import List, Union

def is_sorted_and_how(arr: List[int]) -> str:
    if arr == sorted(arr):
        return 'yes, ascending'
    elif arr == sorted(arr, reverse=True):
        return 'yes, descending'
    else:
        return 'no'

def sort_array(source_array: List[int]) -> List[int]:
    evens = []
    for index in range(0, len(source_array)):
        num = source_array[index]
        if num % 2 == 0:
            evens.append((index, num))
    odds = list(filter(lambda x: x % 2 == 1, source_array))
    sorted_odds = sorted(odds)
    for index, num in evens:
        sorted_odds.insert(index, num)
    return sorted_odds

def sum_mul(n: int, m: int) -> Union[str, int]:
    if n <= 0 or m <= 0:
        return 'INVALID'
    return sum([n * x for x in range(1, int(m / n) + 1) if n * x < m ])

def aplusb(a, b):
    a.__add__(b)

def fizzBuzz(n):
    pre = list(range(1, n + 1))
    ret = []
    for i in pre:
        if i % 15 == 0:
            ret.append("fizz buzz")
        elif i % 3 == 0:
            ret.append("fizz")
        elif i % 5 == 0:
            ret.append("buzz")
        else:
            ret.append(str(i))
    return ret