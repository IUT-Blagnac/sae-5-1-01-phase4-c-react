export default interface CreateSaeForm {
  name: string;
  description: string;
  groups: string[];
  skills: string[];
  teachers: string[];
  minTeamSize: number;
  maxTeamSize: number;
  maxTeamPerSubject: number;
  minTeamPerSubject: number;
  subjects: Topic[];
}

interface Topic {
  name: string;
  description: string;
  category: string[];
}
