/*
ARRAY ---------------------------------------------------------------------------------------------------
*/
int[] array = { 1, 2, 3, 4, 5 };

// Loop
foreach (int element in array) {
    Console.WriteLine(element);   
}

int val3 = array[3]; // Get

array[1] = 7; // Set

// Add value
int[] originalArray = { 1, 2, 3, 4, 5 };
int newElement = 6;
int[] updatedArray = AddElement(originalArray, newElement);

static int[] AddElement(int[] originalArray, int newElement){
    if (originalArray == null) return new int[] { newElement }; // If the original array is null, return a new array with the new element.

    int originalLength = originalArray.Length;
    int[] newArray = new int[originalLength + 1];

    // Copy elements from the original array to the new array
    for (int i = 0; i < originalLength; i++){
        newArray[i] = originalArray[i];
    }
    // Add the new element to the end of the new array
    newArray[originalLength] = newElement;

    return newArray;
}

// Remove element
int[] originalArray2 = { 1, 2, 3, 4, 5 };
int elementToRemove = 3;
int[] updatedArray = RemoveElement(originalArray2, elementToRemove);

static int[] RemoveElement(int[] originalArray2, int elementToRemove){
    if (originalArray2 == null) return originalArray2; // Return null if the original array is null.

    int elementIndex = Array.IndexOf(originalArray2, elementToRemove);

    if (elementIndex == -1){
        // The element was not found in the array, so return the original array as is.
        return originalArray2;
    }

    int[] newArray = new int[originalArray2.Length - 1];
    Array.Copy(originalArray2, 0, newArray, 0, elementIndex);
    Array.Copy(originalArray2, elementIndex + 1, newArray, elementIndex, originalArray2.Length - elementIndex - 1);

    return newArray;
}

/*
LIST ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace T with "int", "string" etc for the type of list you want.
*/
List<T> myList = new List<T>();

// Loop
for (int i = 0; i < myList.Count; i++){
    T item = myList[i];
}

// Foreach loop
foreach (T item in myList){
}

myList.Add(element); // Add

T item = myList[index]; // Get

myList.Remove(element); // Remove

myList.RemoveAt(index); // Remove at index

myList.RemoveAll(item => condition); // Remove if matches condition

bool exists = myList.Contains(element); // Check existence

int index = myList.IndexOf(element); // Find index

myList[index] = newValue; // Set

myList.Sort(); // Sort


/*
DICTIONARY ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace TKey and TValue with "int", "string" etc for the type of key-value pair you want.
*/
Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

// Loop
foreach (var kvp in dictionary)
{
    TKey key = kvp.Key;
    TValue value = kvp.Value;
}


dictionary.Add(key, value); // Add.

dictionary[key] = value; // Overwrites value if key exists, otherwise adds a new key-value pair.

dictionary.Remove(key); // Removes key:value.

TValue value = dictionary[key]; // Get

bool exists = dictionary.ContainsKey(key); // Check key existence.

int count = dictionary.Count; // Returns the number of key-value pairs in the dictionary.

/*
SORTED LIST ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace TKey and TValue with "int", "string" etc for the type of key-value pair you want.
Similar to Dictionary, but keeps pairs sorted by key.
*/

SortedList<TKey, TValue> sortedList = new SortedList<TKey, TValue>();

// Loop 
foreach (var key in sortedList.Keys)
{
    TValue value = sortedList[key];
}

sortedList.Add(key, value); // Add

sortedList.Remove(key); // Remove

TValue value = sortedList[key]; // Get

bool containsKey = sortedList.ContainsKey(key); // Check key existence

bool containsValue = sortedList.ContainsValue(value); // Check value existence

int count = sortedList.Count; // Number of key-value pairs

/*
HASHSET ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace T with "int", "string" etc for the type of value you want.
Every element in Hashset is unique.
*/

HashSet<T> myHashSet = new HashSet<T>();


// Loop 
foreach (var item in myHashSet){
}

myHashSet.Add(item); // Add 

myHashSet.Remove(item); // Remove 

bool exists = myHashSet.Contains(item); // Check existence

int count = myHashSet.Count; // Number of elements

// We can't do GET, we have to use the foreach loop.

/*
SORTEDSET ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace T with "int", "string" etc for the type of value you want.
SortedSet is a data structure that automatically maintains elements in sorted order and does not allow duplicates.
*/

SortedSet<T> sortedSet = new SortedSet<T>();

// Loop
foreach (T element in sortedSet)
{
    // Iterate through the SortedSet in ascending order.
}

// To iterate in descending order, you can use a reverse loop:
for (int i = sortedSet.Count - 1; i >= 0; i--)
{
    T element = sortedSet.ElementAt(i);
    // Iterate through the SortedSet in descending order.
}


sortedSet.Add(element);  // Add

sortedSet.Remove(element); // Remove 
sortedSet.Clear();         // Remove all

bool contains = sortedSet.Contains(element);  // Check existence

T min = sortedSet.Min; // Get Min.
T max = sortedSet.Max; // Get Max.

// Get elements within a specific range [start, end] (inclusive).
var elementsInRange = sortedSet.GetViewBetween(start, end);

// For exclusive range (start < element < end):
var elementsInExclusiveRange = sortedSet.GetViewBetween(start, end).ExceptWith(new[] { start, end });

int count = sortedSet.Count;  // Number of elements


/*
QUEUE ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace T with "int", "string" etc for the type of value you want.
*/

Queue<T> queue = new Queue<T>();

// Loop
foreach (T item in queue){
}


queue.Enqueue(element); // Add

T item = queue.Dequeue(); // Remove (front of queue)

T item = queue.Peek(); // Get front element

bool isEmpty = queue.Count == 0; // Check if empty

bool exists = queue.Contains(element); // Check existence

// Can't access directly element. Can't Set element.


/*
STACK ---------------------------------------------------------------------------------------------------
We can save different type of values in it. Replace T with "int", "string" etc for the type of value you want.
*/

Stack<T> stack = new Stack<T>();

// Loop
foreach (T item in stack){
}


stack.Push(element); // Add

T element = stack.Pop(); // Remove

T topElement = stack.Peek(); // Get

bool isEmpty = stack.Count == 0; // Check if empty

bool exists = stack.Contains(element); // Check existence

int count = stack.Count; // Number of elements

stack.Clear(); // Clear stack



/*
LINKED LIST ---------------------------------------------------------------------------------------------------
*/

LinkedList<string> linkedList = new LinkedList<string>();

// Add elements to the LinkedList
linkedList.AddLast("Apple");
linkedList.AddLast("Banana");
linkedList.AddLast("Cherry");

// Iterating through a LinkedList
foreach (string item in linkedList)
{
    Console.WriteLine(item);
}

// Remove elements:
linkedList.Remove("Banana"); // Remove a specific element
linkedList.RemoveFirst(); // Remove the first element
linkedList.RemoveLast(); // Remove the last element

// Check if an element exists:
bool contains = linkedList.Contains("Cherry");

// Set (modify) an element by finding its node:
LinkedListNode<string> nodeToModify = linkedList.Find("Apple");
if (nodeToModify != null)
{
    nodeToModify.Value = "Apricot";
}

// Use LinkedListNode<T> for more control:
LinkedListNode<string> newNode = linkedList.AddAfter(nodeToModify, "Blueberry"); // Add "Blueberry" after "Apricot"
linkedList.AddBefore(newNode, "Blackberry"); // Add "Blackberry" before "Blueberry"
linkedList.Remove(newNode); // Remove the node containing "Blueberry"

// Additional operations:
int count = linkedList.Count; // Number of elements

linkedList.Clear(); // Remove all 

bool isEmpty = linkedList.Count == 0; // Check if  empty