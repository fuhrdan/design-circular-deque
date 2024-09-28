//*****************************************************************************
//** 641. Design Circular Deque    leetcode                                  **
//*****************************************************************************


typedef struct node {
    int val;
    struct node* next;
    struct node* prev;
} Node;

typedef struct {
    Node* head;
    Node* tail;
    int maxSize;
    int listSize;
} MyCircularDeque;

Node* createNode(int value) {
    Node* newNode = (Node*)malloc(sizeof(Node));
    if (!newNode) {
        return NULL; // Handle memory allocation failure
    }
    newNode->val = value;
    newNode->next = newNode->prev = NULL;
    return newNode;
}

MyCircularDeque* myCircularDequeCreate(int k) {
    MyCircularDeque* obj = (MyCircularDeque*)malloc(sizeof(MyCircularDeque));
    if (!obj) {
        return NULL; // Handle memory allocation failure
    }
    obj->maxSize = k;
    obj->listSize = 0;
    obj->head = obj->tail = NULL;
    return obj;
}

bool myCircularDequeInsertFront(MyCircularDeque* obj, int value) {
    if (obj->listSize == obj->maxSize) {
        return false; // Overflow
    }

    Node* newNode = createNode(value);
    if (!newNode) {
        return false; // Memory allocation failed
    }

    if (!obj->head) { // Empty deque
        obj->head = obj->tail = newNode;
        obj->head->next = obj->head;  // Make it circular
        obj->tail->prev = obj->head;
    } else {
        newNode->next = obj->head;
        obj->head->prev = newNode;
        obj->tail->next = newNode;
        newNode->prev = obj->tail;
        obj->head = newNode;
    }
    
    obj->listSize++;
    return true;
}

bool myCircularDequeInsertLast(MyCircularDeque* obj, int value) {
    if (obj->listSize == obj->maxSize) {
        return false; // Overflow
    }

    Node* newNode = createNode(value);
    if (!newNode) {
        return false; // Memory allocation failed
    }

    if (!obj->head) { // Empty deque
        obj->head = obj->tail = newNode;
        obj->head->next = obj->head;  // Make it circular
        obj->tail->prev = obj->head;
    } else {
        obj->tail->next = newNode;
        newNode->prev = obj->tail;
        newNode->next = obj->head;
        obj->tail = newNode;
        obj->head->prev = obj->tail;
    }
    
    obj->listSize++;
    return true;
}

bool myCircularDequeDeleteFront(MyCircularDeque* obj) {
    if (obj->listSize == 0) {
        return false; // Underflow
    }

    if (obj->listSize == 1) {
        free(obj->head);
        obj->head = obj->tail = NULL;
    } else {
        Node* curr = obj->head;
        obj->head = obj->head->next;
        obj->head->prev = obj->tail;
        obj->tail->next = obj->head;
        free(curr);
    }
    
    obj->listSize--;
    return true;
}

bool myCircularDequeDeleteLast(MyCircularDeque* obj) {
    if (obj->listSize == 0) {
        return false; // Underflow
    }

    if (obj->listSize == 1) {
        free(obj->head);
        obj->head = obj->tail = NULL;
    } else {
        Node* curr = obj->tail;
        obj->tail = obj->tail->prev;
        obj->tail->next = obj->head;
        obj->head->prev = obj->tail;
        free(curr);
    }
    
    obj->listSize--;
    return true;
}

int myCircularDequeGetFront(MyCircularDeque* obj) {
    if (obj->listSize == 0) {
        return -1;
    }
    return obj->head->val;
}

int myCircularDequeGetRear(MyCircularDeque* obj) {
    if (obj->listSize == 0) {
        return -1;
    }
    return obj->tail->val;
}

bool myCircularDequeIsEmpty(MyCircularDeque* obj) {
    return obj->listSize == 0;
}

bool myCircularDequeIsFull(MyCircularDeque* obj) {
    return obj->listSize == obj->maxSize;
}

void myCircularDequeFree(MyCircularDeque* obj) {
    while (obj->listSize > 0) {
        myCircularDequeDeleteFront(obj);
    }
    free(obj);
}

/**
 * Your MyCircularDeque struct will be instantiated and called as such:
 * MyCircularDeque* obj = myCircularDequeCreate(k);
 * bool param_1 = myCircularDequeInsertFront(obj, value);
 
 * bool param_2 = myCircularDequeInsertLast(obj, value);
 
 * bool param_3 = myCircularDequeDeleteFront(obj);
 
 * bool param_4 = myCircularDequeDeleteLast(obj);
 
 * int param_5 = myCircularDequeGetFront(obj);
 
 * int param_6 = myCircularDequeGetRear(obj);
 
 * bool param_7 = myCircularDequeIsEmpty(obj);
 
 * bool param_8 = myCircularDequeIsFull(obj);
 
 * myCircularDequeFree(obj);
*/