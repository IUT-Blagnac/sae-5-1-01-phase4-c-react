import Slider from 'react-slick';
import React, { useState } from 'react';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import {
    Card,
    Typography,
    Box } from "@mui/joy";

    const CarouselFAQ: React.FC = () => {

        const [selectedCard, setSelectedCard] = useState<string | null>(null);

        const settings = {
          dots: true,
          infinite: true,
          slidesToShow: 3,
          slidesToScroll: 3,
          responsive: [
            {
              breakpoint: 1024,
              settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
              },
            },
            {
              breakpoint: 600,
              settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
              },
            },
          ],
        };
      
        return (
            <Box
            sx={{
              margin: "0 3% 0 3%",
              paddingBottom: "1%",
            }}
            >
            <div>
              <Slider {...settings}>
                <div>
                  <Card
                    sx={{
                      margin: "0 1% 0 1%",
                    }}
                  >
                    <Box>
                      <Typography
                        level="h4"
                      >
                        A quoi correspond les états d'une SAE?
                      </Typography>
                      <br/>
                      <Typography>
                        <ul>
                          <li><strong>PENDING USERS</strong> → Fiches personnages à remplir pour la création des équipes</li>
                          <li><strong>PENDING WISHES</strong> → Equipes faisant leurs vœux afin de leur attribuer des sujets</li>
                          <li><strong>LAUNCHED</strong> → Chaque équipe a 1 ou plusieurs sujets, la SAE a commencé</li>
                          <li><strong>LAUNCHED OPEN FOR INTERNSHIP</strong> → Les alternants remplissent leurs fiches de compétences, et une fois validées sont automatiquement intégrés dans une équipe</li>
                          <li><strong>CLOSED</strong> → SAE terminée</li>
                        </ul>
                      </Typography>
                    </Box>
                  </Card>
                </div>
                <div>
                  <Card
                    sx={{
                      margin: "0 1% 0 1%",
                    }}
                  >
                    <Box>
                      <Typography
                        level="h4"
                      >
                        Comment ajouter de nouveaux professeurs et administrateur?
                      </Typography>
                      <br/>
                      <Typography>
                        Dans la version actuelle, l'ajout de professeuresseur et d'administrateur doit être gérer par un développeur, une route d'API sécurisé est dispo pour ajouter de nouveaux professeurs et administrateurs.
                      </Typography>
                    </Box>
                  </Card>
                </div>
                <div>
                  <Card
                    sx={{
                      margin: "0 1% 0 1%",
                    }}
                  >
                    <Box>
                      <Typography>Card 3</Typography>
                      {/* Autres éléments de votre troisième carte */}
                    </Box>
                  </Card>
                </div>
                <div>
                  <Card
                    sx={{
                      margin: "0 1% 0 1%",
                    }}
                  >
                    <Box>
                      <Typography>Card 4</Typography>
                      {/* Autres éléments de votre troisième carte */}
                    </Box>
                  </Card>
                </div>
              </Slider>
            </div>
          </Box>
        );
      };
      
      export default CarouselFAQ;
      