import { User } from "../types/user";

const API_URL = "https://localhost:7290/api/Users";

export async function getUsers(): Promise<User[]> {
  const response = await fetch(API_URL);
  return await response.json();
}

export async function getUser(id: number): Promise<User> {
  const response = await fetch(`${API_URL}/${id}`);
  return await response.json();
}

export const getUserByAuth0Id = async (auth0Id: string) => {
  try {
    const response = await fetch(`${API_URL}/auth0/${auth0Id}`);
    if (!response.ok) {
      if (response.status === 404) {
        // User not found
        return null;
      }
      throw new Error(`Failed to fetch user: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching user by Auth0 ID:", error);
    throw error;
  }
};

export async function createUser(user: User): Promise<User> {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(user),
  });
  return await response.json();
}

export async function updateUser(user: User): Promise<User> {
  const response = await fetch(`${API_URL}/${user.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(user),
  });
  return await response.json();
}

export async function deleteUser(id: number): Promise<User> {
  const response = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
  });
  return await response.json();
}
