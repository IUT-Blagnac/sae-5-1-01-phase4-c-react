import { Link } from "@mui/material";

export default function StudentTab() {
  return (
    <nav>
      <Link
        variant="button"
        color="text.primary"
        href="#"
        sx={{ my: 1, mx: 1.5 }}
      >
        Mes SAE
      </Link>
      <Link
        variant="button"
        color="text.primary"
        href="#"
        sx={{ my: 1, mx: 1.5 }}
      >
        A faire
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
