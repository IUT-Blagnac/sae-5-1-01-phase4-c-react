import { Link } from "@mui/material";

export default function TeacherTab() {
  return (
    <nav>
      <Link
        variant="button"
        color="text.primary"
        href="#"
        sx={{ my: 1, mx: 1.5 }}
      >
        Gestion des SAE
      </Link>
      <Link
        variant="button"
        color="text.primary"
        href="#"
        sx={{ my: 1, mx: 1.5 }}
      >
        Groupes
      </Link>
      <Link
        variant="button"
        color="text.primary"
        href="#"
        sx={{ my: 1, mx: 1.5 }}
      >
        Support
      </Link>
    </nav>
  );
}
