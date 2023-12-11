import APIGetter from "./APIGetter";

export interface SkillCharacter {
  id: string;
  name: string;
  confidence_level: number;
}

export interface Skillzz {
  id: string;
  name: string;
}

export default class SkilzzServices {
  static async getSkilzzFromUserId(userId: string): Promise<SkillCharacter[]> {
    return await APIGetter(`Character/user/${userId}`);
  }

  static async getSkillzz(): Promise<Skillzz[]> {
    return await APIGetter(`Skill`);
  }
}
