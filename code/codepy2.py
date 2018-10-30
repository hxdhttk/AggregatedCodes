def fib(n):
    if n < 2:
        return n
    return fib(n - 1) + fib(n - 2)

def main():
    for n in range(0, 40):
        print("fib (", n, ") = ", fib(n))

if __name__ == "__main__":
    main()