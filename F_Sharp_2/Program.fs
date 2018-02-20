﻿// Learn more about F# at http://fsharp.org

open System

let mulDigits x = 
    let rec mul n acc = 
        if n % 10 = n then acc * n
        else mul (n / 10) (acc * (n % 10))
    mul x 1

let indexOf x list = 
    let rec index i list = 
        match list with
        | [] -> -1
        | [x] -> i
        | h :: t -> if h = x then i 
                    else index (i + 1) t
    index 0 list

let isPalindrome str = 
    let rec checkForEquals (list1, list2) i n = 
        if i = n then true
        else
            match (list1, list2) with
            | ([], []) -> true
            | ([a], [b]) when a = b -> true
            | ([a], [b]) -> false
            | (h1 :: t1, h2 :: t2) -> if h1 <> h2 then false
                                      else checkForEquals (t1, t2) (i + 1) n
    checkForEquals (str, List.rev str) 0 ((List.length str) / 2)

let separateListByTwo n list = 
    let rec separate (left, right) i = 
        if i = n then (left, right)
        else 
            match (left, right) with
            | (x, h2 :: t2) -> separate ((h2 :: (List.rev x)) |> List.rev, t2) (i + 1)
    separate ([], list) 0


let rec mergesort list = 

    let merge left right = 
        let rec mergerec left right res =
            match (left, right) with
            | ([], []) -> res
            | (_, []) -> res @ left
            | ([], _) -> res @ right
            | ([a], [b]) -> if a <= b then res @ [a] @ [b] else res @ [b] @ [a]
            | (h1 :: t1, h2 :: t2) -> if h1 <= h2 then mergerec t1 right (res @ [h1]) 
                                      else mergerec left t2 (res @ [h2])
        mergerec left right []

    let length = List.length list
    if length <= 1 then list
    else 
        let left, right = separateListByTwo (length / 2) list
        merge (mergesort left) (mergesort right)



[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1 - mul digits in number\n2 - index of\n3 - palindrome\n4 - mergesort"
    let problemNumber = Convert.ToInt32(Console.ReadLine())
    if problemNumber = 1 then 
        printfn "Enter a number: "
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "%i" <| mulDigits x
    elif problemNumber = 2 then 
        printfn "Enter the list elem: "
        let list = Console.ReadLine().Split() |> List.ofArray |> List.map (fun x -> int x)
        printfn "Enter the elem to find: "
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "Index of %i in list : %i" <| x <| indexOf x list
    elif problemNumber = 3 then
        printfn "Enter the string: "
        let str = Console.ReadLine()
        printfn "%O" (Seq.toList str |> isPalindrome)
    elif problemNumber = 4 then
        printfn "Enter the list elem: "
        let list = Console.ReadLine().Split() |> List.ofArray |> List.map (fun x -> int x)
        printfn "sorted list: %A" <| mergesort list
    Console.ReadKey() |> ignore

    0 // return an integer exit code