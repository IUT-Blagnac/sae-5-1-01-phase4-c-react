import APIGetter from "./APIGetter";
import APIPoster from "./APIPoster";

export interface Wish {
  id_team: string;
  id_subject: string;
}

export default class WishServices {
  static async getTeamWish(teamId: string): Promise<Wish> {
    const wishes = (await APIGetter(`TeamWish/team/${teamId}`)) as Wish[];
    return wishes[0];
  }

  static async postTeamWish(teamId: string, topicId: string): Promise<any> {
    const body = JSON.stringify({
      team_id: teamId,
      subject_id: topicId,
    });
    return await APIPoster(`TeamWish`, body);
  }
}
