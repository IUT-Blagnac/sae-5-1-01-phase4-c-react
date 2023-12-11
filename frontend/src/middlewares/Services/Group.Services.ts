import APIGetter from "./APIGetter";
import APIPoster from "./APIPoster";

export interface Group {
  id: string;
  name: string;
}

export default class GroupServices {
  static async getGroups(): Promise<Group[]> {
    return await APIGetter(`Group`);
  }

  static async createGroup(
    groupName: string,
    isAlternant: boolean
  ): Promise<any> {
    const body = JSON.stringify({
      name: groupName,
      is_apprenticeship: isAlternant,
    });
    return await APIPoster(`Group`, body);
  }
}
