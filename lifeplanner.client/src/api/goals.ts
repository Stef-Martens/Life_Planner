// import { Goal } from "../types/goal";
// import { useApiClient } from "./apiClient";

// export async function getGoals(userId: number): Promise<Goal[]> {
//   return await apiClient<Goal[]>(`/users/${userId}/goals`);
// }

// export async function createGoal(userId: number, goal: Goal): Promise<Goal> {
//   return await apiClient<Goal>(`/users/${userId}/goals`, {
//     method: "POST",
//     body: JSON.stringify(goal),
//   });
// }

// export async function updateGoal(userId: number, goal: Goal): Promise<Goal> {
//   return await apiClient<Goal>(`/users/${userId}/goals/${goal.id}`, {
//     method: "PUT",
//     body: JSON.stringify(goal),
//   });
// }

// export async function deleteGoal(
//   userId: number,
//   goalId: number
// ): Promise<void> {
//   return await apiClient<void>(`/users/${userId}/goals/${goalId}`, {
//     method: "DELETE",
//   });
// }

import { Goal } from "../types/goal";
import { useApiClient } from "./apiClient";

export function useGoalApi() {
  const apiClient = useApiClient();

  return {
    getGoals: (userId: number) => apiClient<Goal[]>(`/users/${userId}/goals`),
    createGoal: (userId: number, goal: Goal) =>
      apiClient<Goal>(`/users/${userId}/goals`, {
        method: "POST",
        body: JSON.stringify(goal),
      }),
    updateGoal: (userId: number, goal: Goal) =>
      apiClient<Goal>(`/users/${userId}/goals/${goal.id}`, {
        method: "PUT",
        body: JSON.stringify(goal),
      }),
    deleteGoal: (userId: number, goalId: number) =>
      apiClient<void>(`/users/${userId}/goals/${goalId}`, {
        method: "DELETE",
      }),
  };
}
