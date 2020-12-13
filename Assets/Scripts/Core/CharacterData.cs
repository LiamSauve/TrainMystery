using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    //convname, convdesc, convhand
    [System.NonSerialized]
    public string[] dialogueStrings = {
        "Aloquette", "A flirtatious murine escort. The chaotic but affable consort to the late Prince, formerly of the stage. Well-loved by the public for their flamboyant exploits and expensive tastes.", "RIGHT",
        "Bjura", "A mysterious passerine operator. Known widely to be an agent of the Royal Family, but moves with unknown designs. Unflappable and never unprepared.", "RIGHT",
        "Ciandero", "A talented vulpine artist. Low-born, but with a delicate talent at portraiture that has made them the talk of high society, which still overwhelms their humble demeanour.", "RIGHT",
        "Dedan", "The eulipotyphline high chancellor. A high-strung politician whose long career has made plenty of enemies. Fussy, wordy and keen to keep up appearances.", "LEFT",
        "Erovege", "The greasy pantherine compere. A bombastic master of ceremonies who is unable to turn it off. Effervescent and well-known for indulgences.", "RIGHT",
        "Foncura", "A burly caprine blacksmith. Formerly employed by the Royal quartermaster, dismissed under mysterious circumstances. Stout, forthright, and serious about their work.", "RIGHT",
        "Ghidah", "The leporine Pacifican ambassador. A straight-talking public figure, responsible and upright, but dogged by rumours of unsavoury backroom deals.", "RIGHT",
        "Horien", "A delphine high-society dilettante. Friend to the Princess, a social butterfly or ruthless gatekeeper depending on who one asks. Always in a haze of scented smoke.", "LEFT",
        "Iliagros", "The musteline chief policier. A high-profile no-nonsense figure, recognized a mile away for their straight back, few words, and far-too-firm hand.", "RIGHT",
        "Juscenne", "The feline Royal librarian. A bubbly, colourful figure known for their absurd fashions and obscure literary tastes. Always keen to talk. ", "RIGHT",
        "Kastur", "A jovial canine athlete. Childhood friend to the Royal Scions, a kindly and energetic lancer who openly chafes against their luxury by association.", "RIGHT",
        "Lagorei", "A Pacifican vulpine photographer. Fresh from a Royal Gallery exhibition of their travel plates, a restless and motivated aesthete.", "LEFT",
        "Mevecque", "A quiet rhinocerotine butler. A silent and restrained figure familiar to the Royal houses, omnipresent for a significant part of the Royal Scions' life, now dismissed.", "RIGHT",
        "Nyxin", "This is your contact, an ursine Atlantican revolutionary with no love for the Royal Family, but a dedication to seeing that a brighter future is not cut short.", "X",
        "Oirlu", "A respectable columbine concierge. Master of the Royal Arch Hotel, a deferential and studious figure who absolutely must bear some strange and terrible truths.", "RIGHT",
        "Poetian", "The legendary accipitrine general. An old commissioned battle-axe, turned combat instructor to the Prince. Never seen without a cigar and their trusty sabre.", "LEFT",
        "Quivon", "A cheerful anatine typist. Recently gained public technological renown for inventing a highly efficient stenography method. Vibrant and gregarious, with impossibly fast hands.", "RIGHT",
        "Rostrai", "A silent selachine veteran. Seemingly uncomfortable with their status as a war hero, has largely retreated from the public eye. Fastidious, burly, and quietly kind.", "RIGHT",
        "Sienjon", "A diligent macropodine retainer. Fanatically loyal to the Royal Family. Restless like a coiled spring, a fixer with a wide variety of savoury and unsavoury skills.", "RIGHT",
        "Tlalond", "An elephantine philosophy student. Despite their youth, rumoured to be an underground publisher of challenging works. Neurotic, chain-smoking chatterbox.", "LEFT",
        "Ustrade", "The reclusive medusine novelist. Recently enjoying a meteoric rise in popularity due to the rescinding of bans of their works. Always seen observing and noting.", "RIGHT",
        "Verlaux", "This is you, a corvine Pacifican journalist. Schoolmate and confidante of the slain Prince from his days abroad. Out for revenge, clever, but unfamiliar to murder.", "X",
        "Whestam", "A colourful octodonine model. Effortlessly fashionable, strangely alluring, rarely seen outside of train cars or catwalks. Always happy to sign an autograph.", "RIGHT",
        "Xorral", "A sublime cyprine violist. A young musical prodigy, deferential and polite in spite of their successes. Visibly shaky hands and nerves, except when they play.", "LEFT",
        "Yovurah", "The renowned phasianine sculptor. An artiste of international proportions from workmanlike beginnings, uncharacteristically down-to-earth and soft-spoken.", "RIGHT",
        "Zephette", "An awkward equine accountant. Socially inept but diligent and co-operative, recently ascended to the station of youngest-ever member of the Royal Treasury.", "LEFT"
    };

    [System.NonSerialized]
    public string[] notebookPages = {
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "Nyxin:\nFind them. They have something for you.",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "Verlaux:\nShould I not survive this ordeal, let it be known that I came here to avenge a dear friend.",
        "W",
        "X",
        "Y",
        "Z"
    };

    void Start()
    {
        
    }
}
