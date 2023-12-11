import FetcherGET from "./Fetcher";

export interface User {
  id: string;
  email: string;
  first_name: string;
  last_name: string;
  id_roles: number;
}

export default class TeamServices {
  static async getTeamFromUserSae(
    userId: string,
    saeId: string
  ): Promise<{
    idTeam: string;
    nameTeam: string;
    colorTeam: string;
    users: User[];
  }> {
    return await FetcherGET(`Team/${userId}/${saeId}`);
  }
}
