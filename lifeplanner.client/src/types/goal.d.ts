export interface Goal {
  id: number;
  userId: number;
  name: string;
  type: string;
  categoryId: number;
  target: number;
  unit: string;
  description: string;
  dueDate: string;
  createdAt: string;
  isComplete: boolean;
  subtasks: Subtask[];
}
