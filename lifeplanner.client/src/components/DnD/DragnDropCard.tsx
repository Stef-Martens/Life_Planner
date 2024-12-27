// a card that can be dragged and dropped
// has an id, title, and description

import React from "react";
import {useDrag} from "react-dnd";

interface DragnDropCardProps {
    id: string,
    title: string;
    description: string;
}

const DragnDropCard: React.FC<DragnDropCardProps> = ({id, title, description}) => {

    const [{isDragging}, drag] = useDrag(() => ({
        type: "CARD",
        item: {id, title, description},
        collect: (monitor) => ({
            isDragging: monitor.isDragging(),
        }),
    }));

    return (
        <div className="" ref={drag} style={{border: isDragging ? "5px solid orange" : "0px"}}>
            <div className="card-body bg-neutral">
                <h2 className="card-title text-neutral-content">{title}</h2>
                <p className="text-neutral-content">{description}</p>
            </div>
        </div>
    );
};

export default DragnDropCard;