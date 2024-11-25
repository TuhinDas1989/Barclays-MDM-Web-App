export interface Task {
    id: number;
    name: string;
    priority: number;
    status: string;
}

export interface TaskStatus {
    value: string;
    text: string;
}