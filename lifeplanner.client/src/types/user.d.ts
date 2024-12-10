export interface User {
  id?: number;
  auth0Id: string;
  email: string;
  name: string;
  createdAt?: string;
  goals?: Goal[];
  monthlyGoals?: MonthlyGoal[];
  dailyAgendas?: DailyAgenda[];
  dailyTasks?: DailyTask[];
}
