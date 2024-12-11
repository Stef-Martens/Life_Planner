import { useApiClient } from "./apiClient";
import { User } from "../types/user";

export function useUserApi() {
  const apiClient = useApiClient();

  return {
    getUsers: () => apiClient<User[]>(`/users`),
    getUser: (id: number) => apiClient<User>(`/users/${id}`),
    getUserByAuth0Id: async (auth0Id: string): Promise<User | null> => {
      try {
        return await apiClient<User>(
          `/users/auth0/${auth0Id}`,
          {},
          async (response: any) => {
            if (response.status === 404) return null;
            return response.json();
          }
        );
      } catch (error: any) {
        if (error.message.includes("404")) {
          return null;
        }
        throw error;
      }
    },
    createUser: (user: User) =>
      apiClient<User>(`/users`, {
        method: "POST",
        body: JSON.stringify(user),
      }),
    updateUser: (user: User) =>
      apiClient<User>(`/users/${user.id}`, {
        method: "PUT",
        body: JSON.stringify(user),
      }),
    deleteUser: (id: number) =>
      apiClient<User>(`/users/${id}`, {
        method: "DELETE",
      }),
  };
}
